namespace Oceanware.Ocean.Audit {

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;
    using Ocean.Extensions;

    /// <summary>
    /// Class AuditMessageFactory, which provides helper methods to create audit log strings, complete class property values listings in either String or IDictionary formats.
    /// </summary>
    public class AuditMessageFactory {

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditMessageFactory"/> class.
        /// </summary>
        AuditMessageFactory() {
        }

        /// <summary>Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="T:Oceanware.Ocean.Audit.AuditAttribute"/>. If <c>IncludeAllProperties.Yes</c> then all properties will be included with or without the <c>AuditAttribute</c>.</summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="auditFormat">The audit format.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="T:Oceanware.Ocean.Audit.AuditAttribute"/> and the defaultValue is not null or an empty string, then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.
        /// </param>
        /// <returns>IDictionary populated with properties and values.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public static IDictionary<String, String> AuditToIDictionary<T>(T instance, IncludeAllProperties includeAllProperties, SortOption sortOption, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            var dictionary = new Dictionary<String, String>();
            return AuditToIDictionary(instance, dictionary, includeAllProperties, sortOption, auditFormat, defaultValue);
        }

        /// <summary>Populates the dictionary with property's name and value in the class for properties decorated with the <see cref="T:Oceanware.Ocean.Audit.AuditAttribute"/>. If <c>IncludeAllProperties.Yes</c> then all properties will be included with or without the <c>AuditAttribute</c>.</summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated. This could be the Data property of an exception Object that you want to populate, etc.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="auditFormat">The audit format.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="T:Oceanware.Ocean.Audit.AuditAttribute"/> and the defaultValue is not null or an empty string, then a single entry will be added to the dictionary that is named 'DefaultValue' and will have the value of defaultValue.
        /// </param>
        /// <returns>IDictionary populated with properties and values.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown when dictionary is null.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public static IDictionary<String, String> AuditToIDictionary<T>(T instance, IDictionary<String, String> dictionary, IncludeAllProperties includeAllProperties, SortOption sortOption, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            if (dictionary is null) {
                throw new ArgumentNullException(nameof(dictionary));
            }
            if (!Enum.IsDefined(typeof(IncludeAllProperties), includeAllProperties)) {
                throw new InvalidEnumArgumentException(nameof(includeAllProperties), (Int32)includeAllProperties, typeof(IncludeAllProperties));
            }
            if (!Enum.IsDefined(typeof(SortOption), sortOption)) {
                throw new InvalidEnumArgumentException(nameof(sortOption), (Int32)sortOption, typeof(SortOption));
            }
            if (!Enum.IsDefined(typeof(AuditFormat), auditFormat)) {
                throw new InvalidEnumArgumentException(nameof(auditFormat), (Int32)auditFormat, typeof(AuditFormat));
            }

            var list = CreateSortablePropertyBasket(instance, includeAllProperties, sortOption, auditFormat);

            if (list.Count > 0) {
                foreach (var propertyBasket in list) {
                    dictionary.Add(propertyBasket.PropertyName, propertyBasket.Value);
                }
            } else if (!String.IsNullOrWhiteSpace(defaultValue)) {
                dictionary.Add(Constants.DefaultValue, defaultValue);
            }

            return dictionary;
        }

        /// <summary>Builds up a String containing each property and value in the class decorated with the AuditAttribute. The String displays the property name, property friendly name and property value.</summary>
        /// <typeparam name="T">Class type.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="includeAllProperties">The include all properties.</param>
        /// <param name="sortOption">The sort option.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="defaultValue">
        /// If no class properties are decorated with the <see cref="T:Oceanware.Ocean.Audit.AuditAttribute"/> and the defaultValue is not null or an empty string, then the default value will be returned.
        /// </param>
        /// <returns>A String containing each property name, friendly name and value, separated by the delimiter and sorted by AuditAttribute.AuditSequence and then property name.</returns>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value includeAllProperties is not defined.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value sortOption is not defined.</exception>
        /// <exception cref="T:Oceanware.Ocean.ArgumentNullEmptyWhiteSpaceException">Thrown when delimiter is null, empty, or white space.</exception>
        /// <exception cref="InvalidEnumArgumentException">Thrown when enum value auditFormat is not defined.</exception>
        public static String AuditToString<T>(T instance, IncludeAllProperties includeAllProperties, SortOption sortOption, String delimiter = Constants.DefaultAuditMessageDelimiter, AuditFormat auditFormat = AuditFormat.Compact, String defaultValue = Constants.AuditDefaultValue) {
            if (!Enum.IsDefined(typeof(IncludeAllProperties), includeAllProperties)) {
                throw new InvalidEnumArgumentException(nameof(includeAllProperties), (Int32)includeAllProperties, typeof(IncludeAllProperties));
            }
            if (!Enum.IsDefined(typeof(SortOption), sortOption)) {
                throw new InvalidEnumArgumentException(nameof(sortOption), (Int32)sortOption, typeof(SortOption));
            }
            if (String.IsNullOrWhiteSpace(delimiter)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(delimiter));
            }
            var list = CreateSortablePropertyBasket(instance, includeAllProperties, sortOption, auditFormat);
            var sb = new StringBuilder(2048);

            if (list.Count > 0) {

                foreach (var item in list) {
                    sb.Append(item);
                    sb.Append(delimiter);
                }

                if (sb.Length > delimiter.Length) {
                    sb.Length -= delimiter.Length;
                }
            } else if (!String.IsNullOrWhiteSpace(defaultValue)) {
                sb.Append(defaultValue);
            }

            return sb.ToString();
        }

        static IList<AuditPropertyItem> CreateSortablePropertyBasket<T>(T instance, IncludeAllProperties includeAllProperties, SortOption sortOption, AuditFormat auditFormat) {
            if (!Enum.IsDefined(typeof(IncludeAllProperties), includeAllProperties)) {
                throw new InvalidEnumArgumentException(nameof(includeAllProperties), (Int32)includeAllProperties, typeof(IncludeAllProperties));
            }
            if (!Enum.IsDefined(typeof(SortOption), sortOption)) {
                throw new InvalidEnumArgumentException(nameof(sortOption), (Int32)sortOption, typeof(SortOption));
            }
            if (!Enum.IsDefined(typeof(AuditFormat), auditFormat)) {
                throw new InvalidEnumArgumentException(nameof(auditFormat), (Int32)auditFormat, typeof(AuditFormat));
            }
            
            var list = new List<AuditPropertyItem>();
            var nonAttributedPropertyIndex = 1000000;
            foreach (var propInfo in instance.GetType().GetProperties()) {
                var auditAttribute = propInfo.GetCustomAttribute<AuditAttribute>(false);
                if (auditAttribute != null && auditAttribute.SkipAudit == SkipAudit.Yes) {
                    continue;
                }
                var propertyValue = propInfo.GetValue(instance, null);
                String propertyStringValue;
                if (propertyValue == null) {
                    propertyStringValue = Constants.Null;
                } else {
                    propertyStringValue = propertyValue.ToString();
                }
                if (auditAttribute != null) {
                    list.Add(new AuditPropertyItem(auditAttribute.AuditSequence, propInfo.Name, StringExtensions.GetWords(propInfo.Name), propertyStringValue, auditFormat));
                } else if (includeAllProperties == IncludeAllProperties.Yes && propInfo.Name != "Item") {
                    nonAttributedPropertyIndex += 1;
                    list.Add(new AuditPropertyItem(nonAttributedPropertyIndex, propInfo.Name, StringExtensions.GetWords(propInfo.Name), propertyStringValue, auditFormat));
                }
            }

            switch (sortOption) {
                case SortOption.PropertyName:
                    list.Sort(new PropertyNameComparer());
                    break;
                case SortOption.AuditSequencePropertyName:
                    list.Sort(new AuditSequencePropertyNameComparer());
                    break;
                case SortOption.None:
                    break;
                default:
                    throw new InvalidEnumValueException(typeof(SortOption), sortOption);
            }
            return list;
        }
    }
}
