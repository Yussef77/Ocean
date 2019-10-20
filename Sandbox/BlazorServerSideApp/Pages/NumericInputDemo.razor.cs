namespace BlazorServerSideApp.Pages {

    using System;
    using System.Collections.Generic;
    using BlazorServerSideApp.Model;
    using Microsoft.AspNetCore.Components;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "ET001:Type name does not match file name", Justification = "Waiting on Blazor 3.1 RTM to support partial classes so we wouldn't have to do this.")]
    public class NumericInputDemoBase : ComponentBase {

        public Numerics Numerics { get; set; }
        public Boolean ShowModelNameValues { get; private set; }

        public NumericInputDemoBase() {
            this.Numerics = new Numerics();
        }

        public IEnumerable<NameValuePair> GetModelNameValues() {
            var list = new List<NameValuePair>();
            foreach (var pi in this.Numerics.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)) {
                list.Add(new NameValuePair(pi.Name, pi.GetValue(this.Numerics)));
            }
            return list;
        }

        public void HandleValidSubmit() {
            this.ShowModelNameValues = true;
        }

        public void ClearValuesClicked() {
            this.ShowModelNameValues = false;
        }
    }
}
