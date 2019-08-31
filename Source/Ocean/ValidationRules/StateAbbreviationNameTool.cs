namespace Oceanware.Ocean.ValidationRules {

    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class StateAbbreviationNameTool.
    /// </summary>
    /// <remarks>
    /// <para>Unfortunately, the government uses the save abbreviation for four different Armed Forces countries.</para>
    /// <para>Using AE four times really fouls up systems that provide lookup by abbreviation.</para>
    /// <para>When GetStateAbbreviationAndNames is called, all names, including these will be returned in the list.  Use this list for state combo boxes in your UI.</para>
    /// <para>("AE", "Armed Forces Europe")</para>
    /// <para>("AE", "Armed Forces Africa")</para>
    /// <para>("AE", "Armed Forces Canada")</para>
    /// <para>("AE", "Armed Forces Middle East")</para>
    /// </remarks>
    public sealed class StateAbbreviationNameTool {
        static readonly Lazy<StateAbbreviationNameTool> instance = new Lazy<StateAbbreviationNameTool>(() => new StateAbbreviationNameTool());
        readonly Dictionary<String, String> _states = new Dictionary<String, String>();

        /// <summary>
        /// Gets the <seealso cref="StateAbbreviationNameTool"/> instance.
        /// </summary>
        /// <value>The instance.</value>
        public static StateAbbreviationNameTool Instance => instance.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateAbbreviationNameTool"/> class.
        /// </summary>
        StateAbbreviationNameTool() {
            Initialize();
        }

        /// <summary>
        /// Gets the state abbreviation and names. In addition to all valid abbreviations and names, the three additional AE abbreviations and names are added to the list.
        /// </summary>
        /// <returns>IList&lt;KeyValuePair&lt;String, String&gt;&gt;.</returns>
        public IList<KeyValuePair<String, String>> GetStateAbbreviationAndNames() {
            var list = new List<KeyValuePair<String, String>>(_states);
            list.Add(new KeyValuePair<String, String>("AE", "Armed Forces Africa"));
            list.Add(new KeyValuePair<String, String>("AE", "Armed Forces Canada"));
            list.Add(new KeyValuePair<String, String>("AE", "Armed Forces Middle East"));
            return list.OrderBy(x => x.Key).ThenBy(x => x.Value).ToList();
        }

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns>String.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when stateAbbreviation is null, empty, or white space.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when stateAbbreviation is not a valid state abbreviation.</exception>
        public String GetStateName(String stateAbbreviation) {
            if (String.IsNullOrWhiteSpace(stateAbbreviation)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(stateAbbreviation));
            }
            if (IsValid(stateAbbreviation)) {
                return _states[stateAbbreviation.ToUpper()];
            }
            throw new ArgumentOutOfRangeException(nameof(stateAbbreviation), $"{stateAbbreviation} is not a valid state abbreviation.");
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="stateAbbreviation">The state abbreviation.</param>
        /// <returns>Boolean.</returns>
        /// <exception cref="ArgumentNullEmptyWhiteSpaceException">Thrown when stateAbbreviation is null, empty, or white space.</exception>
        public Boolean IsValid(String stateAbbreviation) {
            if (String.IsNullOrWhiteSpace(stateAbbreviation)) {
                throw new ArgumentNullEmptyWhiteSpaceException(nameof(stateAbbreviation));
            }
            return _states.ContainsKey(stateAbbreviation.ToUpper());
        }

        void Initialize() {
            // Leave it up to the government to foul this up!!!
            // Using AE 4 times.  Who's in charge up there!
            //
            // _states.Add("AE", "Armed Forces Europe");
            // _objStates.Add("AE", "Armed Forces Africa")
            // _objStates.Add("AE", "Armed Forces Canada")
            // _objStates.Add("AE", "Armed Forces Middle East")

            _states.Add("AA", "Armed Forces Americas");
            _states.Add("AE", "Armed Forces Europe");
            _states.Add("AK", "Alaska");
            _states.Add("AL", "Alabama");
            _states.Add("AP", "Armed Forces Pacific");
            _states.Add("AR", "Arkansas");
            _states.Add("AS", "American Samoa");
            _states.Add("AZ", "Arizona");
            _states.Add("CA", "California");
            _states.Add("CO", "Colorado");
            _states.Add("CT", "Connecticut");
            _states.Add("DC", "District of Columbia");
            _states.Add("DE", "Delaware");
            _states.Add("FL", "Florida");
            _states.Add("FM", "Federated States of Micronesia");
            _states.Add("GA", "Georgia");
            _states.Add("HI", "Hawaii");
            _states.Add("IA", "Iowa");
            _states.Add("ID", "Idaho");
            _states.Add("IL", "Illinois");
            _states.Add("IN", "Indiana");
            _states.Add("KS", "Kansas");
            _states.Add("KY", "Kentucky");
            _states.Add("LA", "Louisiana");
            _states.Add("MA", "Massachusetts");
            _states.Add("MD", "Maryland");
            _states.Add("ME", "Maine");
            _states.Add("MH", "Marshall Islands");
            _states.Add("MI", "Michigan");
            _states.Add("MN", "Minnesota");
            _states.Add("MO", "Missouri");
            _states.Add("MP", "Northern Mariana Islands");
            _states.Add("MS", "Mississippi");
            _states.Add("MT", "Montana");
            _states.Add("NC", "North Carolina");
            _states.Add("ND", "North Dakota");
            _states.Add("NE", "Nebraska");
            _states.Add("NH", "New Hampshire");
            _states.Add("NJ", "New Jersey");
            _states.Add("NM", "New Mexico");
            _states.Add("NV", "Nevada");
            _states.Add("NY", "New York");
            _states.Add("OH", "Ohio");
            _states.Add("OK", "Oklahoma");
            _states.Add("OR", "Oregon");
            _states.Add("PA", "Pennsylvania");
            _states.Add("PR", "Puerto Rico");
            _states.Add("PW", "Palau");
            _states.Add("RI", "Rhode Island");
            _states.Add("SC", "South Carolina");
            _states.Add("SD", "South Dakota");
            _states.Add("TN", "Tennessee");
            _states.Add("TX", "Texas");
            _states.Add("UT", "Utah");
            _states.Add("VA", "Virginia");
            _states.Add("VI", "Virgin Islands");
            _states.Add("VT", "Vermont");
            _states.Add("WA", "Washington");
            _states.Add("WI", "Wisconsin");
            _states.Add("WV", "West Virginia");
            _states.Add("WY", "Wyoming");
        }
    }
}
