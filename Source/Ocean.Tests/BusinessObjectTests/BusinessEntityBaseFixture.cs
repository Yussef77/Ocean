namespace Ocean.Tests.BusinessObjectTests {

    using System;
    using Newtonsoft.Json;
    using Oceanware.Ocean.BusinessObject;
    using Oceanware.Ocean.InputStringRules;
    using Oceanware.Ocean.ValidationRules;
    using Xunit;

    public class BusinessEntityBaseFixture {
        readonly Customer _sut;

        public BusinessEntityBaseFixture() {
            _sut = new Customer();
        }

        [Fact]
        public void WhenEntityActiveRuleSetIsSetValueIsPersisted() {
            // arrange
            _sut.ActiveRuleSet = ValidationConstants.Delete;

            // act

            // assert
            Assert.True(_sut.ActiveRuleSet == ValidationConstants.Delete, "ActiveRule set should have been Delete");
        }

        [Fact]
        public void WhenEntityIsCreatedAndCheckAllRulesIsRunBrokenValidationRulesShouldBeCorrect() {
            // arrange
            const String ExpectedFirstNamePropertyError = "First Name minimum length is 3.";
            const String ExpectedLastNamePropertyError = "Last Name null value is not allowed.";
            const String ExpectedError = "First Name minimum length is 3.\r\nLast Name null value is not allowed.";
            // act
            _sut.CheckAllRules();

            // assert
            Assert.True(_sut.IsDirty == false, "IsDirty should have been false");
            Assert.True(_sut.IsNotValid == true, "IsNotValid should have been true");
            Assert.True(_sut.IsValid == false, "IsValid should have been false");
            Assert.True(_sut.Error == ExpectedError, $"Error should have been {ExpectedError}");
            Assert.True(_sut[nameof(Customer.FirstName)] == ExpectedFirstNamePropertyError, $"Index should have been {ExpectedFirstNamePropertyError}");
            Assert.True(_sut[nameof(Customer.LastName)] == ExpectedLastNamePropertyError, $"Index should have been {ExpectedLastNamePropertyError}");
        }

        [Fact]
        public void WhenEntityIsCreatedDefaultsAreCorrect() {
            // arrange
            var sut = new CustomerWrapper();

            // act

            // assert
            Assert.True(sut.ActiveRuleSet == ValidationConstants.Insert, "ActiveRule set should have been Insert");
            Assert.True(sut.Error == String.Empty, "Error should have been empty string");
            Assert.True(sut.IsDirty == false, "IsDirty should have been false");
            Assert.True(sut.IsDuplicateRow == false, "IsDuplicateRow should have been false");
            Assert.True(sut.IsLoading == false, "IsLoading should have been false");
            Assert.True(sut.IsNotValid == false, "IsNotValid should have been false");
            Assert.True(sut.IsValid == true, "IsValid should have been true");
            Assert.True(sut.MarkedForDeletion == false, "MarkedForDeletion should have been false");
            Assert.True(sut.RowNumber == 0, "RowNumber should have been zero");
            Assert.True(sut.AddInstanceBusinessValidationRulesCalled, "AddInstanceBusinessValidationRules should have been called");
            Assert.True(sut.AddSharedBusinessValidationRulesCalled, "AddSharedBusinessValidationRules should have been called");
            Assert.True(sut.AddSharedCharacterCasingFormattingRulesCalled, "AddSharedCharacterCasingFormattingRules should have been called");
        }

        [Fact]
        public void WhenEntityIsDeserializingPropertyMethodsAreInvoked() {
            // arrange
            const String Value = "value";
            const String ExpectedValue = "Value";

            _sut.FirstName = Value;

            // act
            var serialized = JsonConvert.SerializeObject(_sut);
            var deserialized = JsonConvert.DeserializeObject<Customer>(serialized);

            deserialized.CheckAllRules();

            // assert
            Assert.True(deserialized.IsDirty == false, "IsDirty should have been false");
            Assert.True(_sut.FirstName == ExpectedValue);
            Assert.True(_sut[nameof(Customer.FirstName)] == String.Empty, $"First Name Index should have been empty string");
        }

        [Fact]
        public void WhenEntityIsDuplicateRowIsSetValueIsPersistedAndINPCRaised() {
            // arrange

            // act

            // assert
            Assert.PropertyChanged(_sut, nameof(BusinessEntityBase.IsDuplicateRow), () => _sut.IsDuplicateRow = true);
            Assert.True(_sut.IsDuplicateRow == true);
        }

        [Fact]
        public void WhenEntityIsLoadedThenEndLoadingCalledEntityIsInCorrectState() {
            // arrange
            const String Value = "value";
            const String ExpectedLastNamePropertyError = "Last Name null value is not allowed.";

            // act
            _sut.BeginLoading();
            _sut.FirstName = Value;
            _sut.EndLoading();
            _sut.CheckAllRules();

            // assert
            Assert.True(_sut.IsDirty == false, "IsDirty should have been false");
            Assert.True(_sut.IsNotValid == true, "IsNotValid should have been true");
            Assert.True(_sut.IsValid == false, "IsValid should have been false");
            Assert.True(_sut.Error == ExpectedLastNamePropertyError, $"Error should have been {ExpectedLastNamePropertyError}");
            Assert.True(_sut[nameof(Customer.FirstName)] == String.Empty, $"First Name Index should have been empty string");
            Assert.True(_sut[nameof(Customer.LastName)] == ExpectedLastNamePropertyError, $"Index should have been {ExpectedLastNamePropertyError}");
            Assert.True(_sut.FirstName == Value, $"First Name should have been {Value}");
        }

        [Fact]
        public void WhenEntityIsLoadingAndMethodOrPropertyReadThrowsInvalidOperationException() {
            // arrange

            // act
            _sut.BeginLoading();

            // assert
            Assert.Throws<InvalidOperationException>(() => _sut.Error);
            Assert.Throws<InvalidOperationException>(() => _sut.InstanceValidationRulesManager);
            Assert.Throws<InvalidOperationException>(() => _sut.IsDirty);
            Assert.Throws<InvalidOperationException>(() => _sut.IsNotValid);
            Assert.Throws<InvalidOperationException>(() => _sut.IsValid);
            Assert.Throws<InvalidOperationException>(() => _sut[nameof(Customer.FirstName)]);
            Assert.Throws<InvalidOperationException>(() => _sut.IsValid);
            Assert.Throws<InvalidOperationException>(() => _sut.CheckAllRules());
            Assert.Throws<InvalidOperationException>(() => _sut.CheckRulesForProperty(nameof(Customer.FirstName)));
        }

        [Fact]
        public void WhenEntityMarkedForDeletionIsSetValueIsPersistedAndINPCRaised() {
            // arrange

            // act

            // assert
            Assert.PropertyChanged(_sut, nameof(BusinessEntityBase.MarkedForDeletion), () => _sut.MarkedForDeletion = true);
            Assert.True(_sut.MarkedForDeletion == true);
        }

        [Fact]
        public void WhenEntityPropertyIsSetIsCorrectlyEntityIsValidAndINPCRaised() {
            // arrange
            const String Value = "value";
            const String ExpectedValue = "Value";

            // act

            // assert
            Assert.PropertyChanged(_sut, nameof(Customer.FirstName), () => _sut.FirstName = Value);
            Assert.True(_sut.IsDirty == true);
            Assert.True(_sut.IsNotValid == false);
            Assert.True(_sut.IsValid == true);
            Assert.True(_sut.Error == String.Empty);
            Assert.True(_sut[nameof(Customer.FirstName)] == String.Empty);
            Assert.True(_sut.FirstName == ExpectedValue);
        }

        [Fact]
        public void WhenEntityPropertyIsSetIsDirtyIsTrue() {
            // arrange
            _sut.FirstName = "value";
            // act

            // assert
            Assert.True(_sut.IsDirty == true);
        }

        [Fact]
        public void WhenEntityPropertyIsSetIsIncorrectlyEntityIsNotValidAndINPCRaised() {
            // arrange
            const String Value = "v";
            const String ExpectedValue = "V";
            const String ExpectedPropertyError = "First Name minimum length is 3.";

            // act

            // assert
            Assert.PropertyChanged(_sut, nameof(Customer.FirstName), () => _sut.FirstName = Value);
            Assert.True(_sut.IsDirty == true, "IsDirty should have been true");
            Assert.True(_sut.IsNotValid == true, "IsNotValid should have been true");
            Assert.True(_sut.IsValid == false, "IsValid should have been false");
            Assert.True(_sut.Error == ExpectedPropertyError, $"Error should have been {ExpectedPropertyError}");
            Assert.True(_sut[nameof(Customer.FirstName)] == ExpectedPropertyError, $"Index should have been {ExpectedPropertyError}");
            Assert.True(_sut.FirstName == ExpectedValue);
        }

        [Fact]
        public void WhenEntityRowNumberIsSetValueIsPersisted() {
            // arrange
            const Int32 ExpectedRowNumber = 1012;

            // act
            _sut.RowNumber = ExpectedRowNumber;

            // assert
            Assert.True(_sut.RowNumber == ExpectedRowNumber);
        }

        class Customer : BusinessEntityBase {
            String _firstName = String.Empty;

            String _lastName;
            public Boolean AddInstanceBusinessValidationRulesCalled { get; private set; }

            public Boolean AddSharedBusinessValidationRulesCalled { get; private set; }

            public Boolean AddSharedCharacterCasingFormattingRulesCalled { get; private set; }

            public String AfterPropertyChangedCalled { get; private set; }

            public String BeforePropertyChangedCalled { get; private set; }

            [CharacterFormatting(CharacterCasing.ProperName)]
            [StringLengthValidator(3, 20)]
            public String FirstName {
                get { return _firstName; }
                set { base.SetPropertyValue(ref _firstName, value); }
            }

            [CharacterFormatting(CharacterCasing.ProperName)]
            [StringLengthValidator(3, 20)]
            public String LastName {
                get { return _lastName; }
                set { base.SetPropertyValue(ref _lastName, value); }
            }

            public Customer() {
            }

            protected override void AddInstanceBusinessValidationRules() {
                base.AddInstanceBusinessValidationRules();
                this.AddInstanceBusinessValidationRulesCalled = true;
            }

            protected override void AddSharedBusinessValidationRules(ValidationRulesManager validationRulesManager) {
                base.AddSharedBusinessValidationRules(validationRulesManager);
                this.AddSharedBusinessValidationRulesCalled = true;
            }

            protected override void AddSharedCharacterCasingFormattingRules(CharacterFormattingRulesManager characterCasingRulesManager) {
                base.AddSharedCharacterCasingFormattingRules(characterCasingRulesManager);
                this.AddSharedCharacterCasingFormattingRulesCalled = true;
            }

            protected override void AfterPropertyChanged(String propertyName) {
                base.AfterPropertyChanged(propertyName);
                this.AfterPropertyChangedCalled = propertyName;
            }

            protected override void BeforePropertyChanged(String propertyName) {
                base.BeforePropertyChanged(propertyName);
                this.BeforePropertyChangedCalled = propertyName;
            }
        }

        class CustomerWrapper : Customer {

            public CustomerWrapper() {
            }
        }
    }
}
