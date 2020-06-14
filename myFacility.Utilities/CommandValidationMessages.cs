using System;
using System.Collections.Generic;
using System.Text;

namespace myFacility.Utilities
{
    public class CommandValidationMessages
    {
        public const string NotEmpty = "Please ensure you have entered the {0}";
        /// <summary>
        /// {0} - Field Name
        /// </summary>
        public const string IsAlphabetOnly = "Field {0} accepts letters only";
        public const string IsAlphabetNumericOnly = "Field {0} accepts letters and numbers only";
        public const string IsNumbersOnly = "Field {0} accepts digit only";
        public const string IsAlphabetCommaPeriodEsclamationMarkAndQuestionMaryOnly = "Field {0} accepts letters, numbers, commas, !, and ? only";
        public const string ValidPhoneNumber = "Please provide valid phone number";
        public const string ValidEmail = "Please provide valid email";
        public const string isAcceptableCharactersOnly = "Field {0} accepts {1} only";
        public const string isGreaterThan = "Field only accepts value greater than {0}";
        public const string isCountAboveOne = "Select at least one {0} ";
        /// <summary>
        /// {0} - FieldName
        /// {1} - Min Character
        /// {2} - Max Character
        /// </summary>
        public const string ExpectedMinMaxCharacters = "Field {0} must have between {1} and {2} Characters";

        public const string PriceInDecimal = "{0} must not be more than {1} digit in total, whith allowance of {2} decimals";
    }

    
        
    
}
