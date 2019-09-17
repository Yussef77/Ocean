namespace Oceanware.Ocean.BusinessObject {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Text;
    using Newtonsoft.Json;
    using Oceanware.Ocean.Audit;
    using Oceanware.Ocean.Infrastructure;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.Rules;
    using Oceanware.Ocean.ValidationRules;

    /// <summary>
    /// Class BusinessEntityBase.
    /// <para>This class is typically used in applications where IDataErrorInfo can be utilized.  For example, WPF, UWP, Xamarin.Forms.</para>
    /// <para>The class provides a rich set of services for application entities or business objects.</para>
    /// </summary>
    /// <seealso cref="ObservableObject" />
    /// <seealso cref="IDataErrorInfo" />
    /// <seealso cref="ILoadable" />
    /// <seealso cref="IRuleSet" />
    [Serializable]
    public abstract class BusinessEntityBase : ObservableObject, IDataErrorInfo, ILoadable, IRuleSet, ISupportInstanceValidationRules {
        static readonly Object _LockObject = new Object();
        String _activeRuleSet = ValidationConstants.Insert;

        [field: NonSerialized]
        ValidationRulesManager _instanceValidationRulesManager;

        [field: NonSerialized]
        IModelRulesInvoker _modelRulesInvoker;

        Boolean _isDirty;
        Boolean _isDuplicateRow;
        Boolean _markedForDeletion;

        [field: NonSerialized]
        readonly BrokenValidationRules _entityBrokenValidationRules = new BrokenValidationRules();

        /// <summary>
        /// Gets or sets the active rule set. Use this property to have a specific rule set checked in addition to all rules that are not assigned a specific rule set.  For example, if you set this property to, Insert; when the rules are checked, all general rules will be checked and the Insert rules will also be checked.
        /// </summary>
        /// <value>The active rule set.</value>
        /// <exception cref="InvalidOperationException">Active rule set can not contain any pipe symbols. This is normally caused when a validation constant is passed that is for multiple rule sets and not a single rule set that this property requires.</exception>
        [Browsable(false)]
        public String ActiveRuleSet {
            get {
                return _activeRuleSet;
            }
            set {
                if (value.Contains("|")) {
                    throw new InvalidOperationException(Strings.ActiveRuleSetCanNotContainPipeSymbols);
                }
                _activeRuleSet = value;
            }
        }

        [JsonIgnore]
        IModelRulesInvoker ModelRulesInvoker {
            get {
                return _modelRulesInvoker ?? (_modelRulesInvoker = new ModelRulesInvoker());
            }
        }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this Object.  Every error in the Broken Rules collection is returned.
        /// </summary>
        /// <value>The error.</value>
        /// <exception cref="InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <exception cref="InvalidOperationException">if <see cref="BusinessEntityBase.IsLoading" /> is <c>true</c>.</exception>
        /// <returns>String that represents all broken rule descriptions for this instance.</returns>
        [Browsable(false)]
        [JsonIgnore]
        public String Error {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }
                var sb = new StringBuilder(4096);

                foreach (var item in _entityBrokenValidationRules.GetBrokenRules().OrderBy(x => x.PropertyName).ThenBy(x => x.RuleTypeName)) {
                    sb.AppendLine(item.ErrorMessage);
                }

                if (sb.Length > 2) {
                    sb.Length -= 2;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Used by this base class internally as a method of getting the Instance Validation RulesManager.  This property uses lazy Object creation.
        /// </summary>
        /// <value>The instance validation rules manager.</value>
        /// <exception cref="InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <exception cref="InvalidOperationException">if <see cref="BusinessEntityBase.IsLoading" /> is <c>true</c>.</exception>
        /// <returns><see cref="ValidationRulesManager" /> for this instance.</returns>
        [JsonIgnore]
        public ValidationRulesManager InstanceValidationRulesManager {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }

                return _instanceValidationRulesManager ?? (_instanceValidationRulesManager = new ValidationRulesManager());
            }
        }

        /// <summary>
        /// Is this Object dirty? Have changes been made since the Object was loaded or a new Object was constructed. This is automatically kept track of by this base class.
        /// </summary>
        /// <value>The is dirty.</value>
        /// <exception cref="InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is dirty; otherwise <c>false</c>.</returns>
        [Browsable(false)]
        public Boolean IsDirty {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }

                return _isDirty;
            }
            private set {
                _isDirty = value;
                InternalRaisePropertyChanged(nameof(IsDirty));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is duplicate row.
        /// </summary>
        /// <value><c>true</c> if this instance is duplicate row; otherwise, <c>false</c>.</value>
        public Boolean IsDuplicateRow {
            get {
                return _isDuplicateRow;
            }
            set {
                _isDuplicateRow = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is loading.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is loading; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [JsonIgnore]
        public Boolean IsLoading { get; private set; }

        /// <summary>
        /// Is this Object not valid? Property is exposed for data binding purposes so that consumers do not need to use a converter when requiring a negative true result.
        /// </summary>
        /// <value>The is not valid.</value>
        /// <exception cref="InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is not valid; otherwise <c>false</c>.</returns>
        [Browsable(false)]
        public Boolean IsNotValid {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }

                return !this.IsValid;
            }
        }

        /// <summary>
        /// Is this Object valid?
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        /// <exception cref="InvalidOperationException">EndLoading never called after a BeginLoading call was made. No operations are permitted until EndLoading has been called.</exception>
        /// <returns><c>True</c> if this instance is valid; otherwise <c>false</c>.</returns>
        [Browsable(false)]
        public Boolean IsValid {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }

                return !_entityBrokenValidationRules.HasErrors;
            }
        }

        /// <summary>
        /// Gets a multi-line error message indicating what is wrong with this property. Every error for this property in the Broken Rules collection is returned.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>String containing all error messages for the property; if the property is valid returns an empty string.</returns>
        [Browsable(false)]
        [JsonIgnore]
        public String this[String columnName] {
            get {
                if (this.IsLoading) {
                    throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
                }
                var sb = new StringBuilder(1024);
                foreach (var item in _entityBrokenValidationRules.GetBrokenRules(columnName).OrderBy(x => x.PropertyName).ThenBy(x => x.RuleTypeName)) {
                    sb.AppendLine(item.ErrorMessage);
                }
                if (sb.Length > 2) {
                    sb.Length -= 2;
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been marked for deletion.
        /// </summary>
        /// <value><c>true</c> if this instance has been marked for deletion; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public Boolean MarkedForDeletion {
            get {
                return _markedForDeletion;
            }
            set {
                _markedForDeletion = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the row number. Used when this instance is part of a collection. Row number is set after the collection is populated.
        /// </summary>
        /// <value>The row number.</value>
        [Browsable(false)]
        public Int32 RowNumber { get; set; }

        /// <summary>
        /// Initializes the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        static BusinessEntityBase() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessEntityBase"/> class.
        /// </summary>
        /// <remarks>
        /// Constructor sets the <see cref="BusinessEntityBase.ActiveRuleSet"/> to <see cref="String.Empty"/>, invokes AddInstanceBusinessValidationRules and AddSharedBusinessRules./>
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        protected BusinessEntityBase() {
            Initialize();
            AddInstanceBusinessValidationRules();
            AddSharedBusinessRules();
        }

        /// <summary>
        /// Override this method to set up event handlers so user code in a partial class can respond to events raised by generated code.
        /// </summary>
        protected virtual void Initialize() {
        }

        /// <summary>
        /// Validates the entity against all shared and instance rules.
        /// </summary>
        public void CheckAllRules() {
            if (this.IsLoading) {
                throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
            }
            _entityBrokenValidationRules.Clear();

            var validationResult = this.ModelRulesInvoker.CheckAllValidationRules(this);
            _entityBrokenValidationRules.Add(validationResult);
            RaisePropertyChanged(nameof(Error));

            if (_entityBrokenValidationRules.HasErrors) {
                InternalRaisePropertyChanged(nameof(Error));
                InternalRaisePropertyChanged(nameof(IsValid));
                InternalRaisePropertyChanged(nameof(IsNotValid));
            }
        }

        /// <summary>
        /// Validates the property against all shared and instance rules for the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void CheckRulesForProperty(String propertyName) {
            if (this.IsLoading) {
                throw new InvalidOperationException(Strings.EndLoadingNeverCalledAfterBeginLoading);
            }
            _entityBrokenValidationRules.Remove(propertyName);
            var errorCount = _entityBrokenValidationRules.ErrorCount;
            var validationResult = this.ModelRulesInvoker.CheckAllValidationRulesForProperty(this, propertyName);
            _entityBrokenValidationRules.Add(validationResult);

            if (_entityBrokenValidationRules.ErrorCount > errorCount) {
                InternalRaisePropertyChanged(nameof(Error));
                InternalRaisePropertyChanged(nameof(IsValid));
                InternalRaisePropertyChanged(nameof(IsNotValid));
            }
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// Use the instance method, AddInstanceRule to in deriving classes to add instance rules to the Object.
        /// </summary>
        protected virtual void AddInstanceBusinessValidationRules() {
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED business rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared rules to business objects to deriving class properties, override this method in deriving classes and add the rules to the ValidationRulesManager.
        /// This method will only be called once; the first time the deriving class is created.
        /// </summary>
        /// <param name="validationRulesManager">The validation rules manager.</param>
        /// <example>mgrValidation.AddRule(ComparisonValidationRules.CompareValueRule, new CompareValueRuleDescriptor(ComparisonType.LessThan, RequiredEntry.No, DateTime.Today, String.Empty, "Birthday", "Birthday", String.Empty, String.Empty), RuleType.Shared);</example>
        protected virtual void AddSharedBusinessValidationRules(ValidationRulesManager validationRulesManager) {
        }

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up SHARED character casing rules.  This method is only used by the sub-class and not consumers of the sub-class.
        /// To add shared character case formatting to deriving class properties, override this method in deriving classes and add the rules to the CharacterCasingRulesManager.
        /// This method will only be called once; the first time the deriving class is created.
        /// </summary>
        /// <param name="characterCasingRulesManager">The character casing rules manager.</param>
        protected virtual void AddSharedCharacterCasingFormattingRules(CharacterFormattingRulesManager characterCasingRulesManager) {
        }

        /// <summary>
        /// This code gets called on the first time this Object type is constructed
        /// </summary>
        void AddSharedBusinessRules() {
            lock (_LockObject) {
                var entityType = this.GetType();
                if (!SharedValidationRules.RulesExistFor(entityType)) {
                    ValidationRulesManager mgrValidation = SharedValidationRules.GetManager(entityType);
                    if (mgrValidation.RulesLoaded) {
                        return;
                    }
                    mgrValidation.SetRulesLoaded();
                    CharacterFormattingRulesManager mgrCharacterCasing = SharedCharacterFormattingRules.GetManager(entityType);

                    foreach (PropertyInfo prop in entityType.GetProperties()) {
                        foreach (BaseValidatorAttribute atr in prop.GetCustomAttributes(typeof(BaseValidatorAttribute), false)) {
                            mgrValidation.AddRule(atr, prop.Name);
                        }

                        foreach (CharacterFormattingAttribute atr in prop.GetCustomAttributes(typeof(CharacterFormattingAttribute), false)) {
                            mgrCharacterCasing.AddRule(prop.Name, atr.CharacterCasing, atr.RemoveSpace, atr.PhoneExtension);
                        }
                    }

                    AddSharedBusinessValidationRules(mgrValidation);
                    AddSharedCharacterCasingFormattingRules(mgrCharacterCasing);
                }
            }
        }

        /// <summary>
        /// Derived classes can override this method to execute logic after the property is set. The base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void AfterPropertyChanged(String propertyName) {
        }

        /// <summary>
        /// Derived classes can override this method to execute logic before the property is set. The base implementation does nothing.
        /// </summary>
        /// <param name="propertyName">
        /// The property which was changed.
        /// </param>
        protected virtual void BeforePropertyChanged(String propertyName) {
        }

        /// <summary>
        /// Called by in business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state:
        /// <p>    this method runs validation rules on the property</p>
        /// <p>    this method formats the property value using the character case rule</p>
        ///
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(databaseConnection, Value)
        ///   End Set
        /// </code>
        /// Or
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(databaseConnection, Value, "DatabaseConnection")
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="currentValue">Current property value.</param>
        /// <param name="newValue">Value argument from the property setter.</param>
        /// <param name="propertyName">Optional, name of the property.</param>
        protected void SetPropertyValue(ref String currentValue, String newValue, [CallerMemberName] String propertyName = null) {
            if (currentValue == null) {
                if (newValue == null) {
                    return;
                }
            } else if (newValue != null && currentValue.Equals(newValue)) {
                return;
            }

            if (!this.IsLoading) {
                this.IsDirty = true;
                this.BeforePropertyChanged(propertyName);
                if (newValue != null) {
                    currentValue = this.ModelRulesInvoker.FormatPropertyValueUsingCharacterCaseRule(this, propertyName, newValue);
                } else {
                    currentValue = newValue;
                }
                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);
                RaisePropertyChanged(nameof(Error));
            } else {
                currentValue = newValue;
            }
        }

        /// <summary>
        /// Called by business entity sub-classes in their property setters to set the value of the property.
        /// If the business Object is not in a loading state, this method runs validation rules on the property.
        /// <example>Example:
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(_birthday, Value)
        ///   End Set
        /// </code>
        /// Or
        /// <code>
        ///   Set(ByVal Value As String)
        ///       MyBase.SetPropertyValue(_birthday, Value, "Birthday")
        ///   End Set
        /// </code>
        /// </example>
        /// </summary>
        /// <typeparam name="T">Property Type.</typeparam>
        /// <param name="currentValue">variable that holds the current value of the property.</param>
        /// <param name="newValue">Value argument from the property setter.</param>
        /// <param name="propertyName">Optional, name of the property.</param>
        protected void SetPropertyValue<T>(ref T currentValue, T newValue, [CallerMemberName] String propertyName = null) {
            if (currentValue == null) {
                if (newValue == null) {
                    return;
                }
            } else if (newValue != null && currentValue.Equals(newValue)) {
                return;
            }

            if (!this.IsLoading) {
                this.IsDirty = true;
                this.BeforePropertyChanged(propertyName);
                currentValue = newValue;
                CheckRulesForProperty(propertyName);
                InternalRaisePropertyChanged(propertyName);
                RaisePropertyChanged(nameof(Error));
            } else {
                currentValue = newValue;
            }
        }

        void InternalRaisePropertyChanged(String propertyName) {
            if (!this.IsLoading) {
                RaisePropertyChanged(propertyName);
            }
            this.AfterPropertyChanged(propertyName);
        }

        /// <summary>
        /// Called when the business Object is being loaded from a database.  This saves time and processing; by passing property setter logic during loading.  After the business Object has been loaded the EndLoading MUST be called.
        /// </summary>
        public void BeginLoading() {
            this.IsLoading = true;
        }

        /// <summary>
        /// After a business Object has been loaded and the BeginLoading method was called, developers must call this method, EndLoading.  This method marks the entity IsDirty = False, HasBeenValidated = False and raises these property changed events.
        /// </summary>
        public void EndLoading() {
            this.IsLoading = false;
            this.IsDirty = false;
        }

        /// <summary>Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>. If <c>IncludeAllProperties.Yes</c> then all properties will be included with or without the <c>AuditAttribute</c>.</summary>
        /// <param name="instance">The instance.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="auditFormat">The audit format.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="AuditAttribute"/> and the defaultValue is not null or an empty string, then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.
        /// </param>
        /// <returns>IDictionary populated with properties and values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public IDictionary<String, String> AuditToIDictionary(IncludeAllProperties includeAllProperties, SortOption sortOption, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            return AuditMessageFactory.AuditToIDictionary(this, includeAllProperties, sortOption, auditFormat, defaultValue);
        }

        /// <summary>Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="AuditAttribute"/>. If <c>IncludeAllProperties.Yes</c> then all properties will be included with or without the <c>AuditAttribute</c>.</summary>
        /// <param name="instance">The instance.</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated. This could be the Data property of an exception Object that you want to populate, etc.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="auditFormat">The audit format.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="AuditAttribute"/> and the defaultValue is not null or an empty string, then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.
        /// </param>
        /// <returns>IDictionary populated with properties and values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public IDictionary<String, String> AuditToIDictionary(IDictionary<String, String> dictionary, IncludeAllProperties includeAllProperties, SortOption sortOption, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            return AuditMessageFactory.AuditToIDictionary(this, dictionary, includeAllProperties, sortOption, auditFormat, defaultValue);
        }

        /// <summary>Builds up a String containing each property and value in the class decorated with the AuditAttribute. The String displays the property name, property friendly name and property value.</summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="AuditAttribute"/> and the defaultValue is not null or an empty string, then the default value will be returned.
        /// </param>
        /// <returns>A String containing each property name, friendly name and value, separated by the delimiter and sorted by AuditAttribute.AuditSequence and then property name.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when delimiter is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public String AuditToString<T>(IncludeAllProperties includeAllProperties, SortOption sortOption, String delimiter = Constants.DefaultAuditMessageDelimiter, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            return AuditMessageFactory.AuditToString(this, includeAllProperties, sortOption, delimiter, auditFormat, defaultValue);
        }

        /// <summary>
        /// Called after a successful update or insert. This method sets IsDirty to false;
        /// </summary>
        public void Updated() {
            this.IsDirty = false;
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context) {
            this.BeginLoading();
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context) {
            this.EndLoading();
        }
    }
}
