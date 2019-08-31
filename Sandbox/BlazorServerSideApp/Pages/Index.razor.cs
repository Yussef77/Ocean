﻿namespace BlazorServerSideApp.Pages {

    using System;
    using BlazorServerSideApp.Model;
    using Microsoft.AspNetCore.Components;
    using Oceanware.Ocean.ValidationRules;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "ET001:Type name does not match file name", Justification = "Waiting on Blazor to have partial classes so we wouldn't have to do this.")]
    public class IndexBase : ComponentBase {
        public String CurrentRuleSet { get { return $"Current Rule Set {this.Customer.ActiveRuleSet}"; } }
        public Customer Customer { get; set; }

        public IndexBase() {
            this.Customer = new Customer();
            this.Customer.ActiveRuleSet = ValidationConstants.Insert;
        }

        public void HandleValidSubmit() {
            Console.WriteLine("OnValidSubmit");
        }
    }
}