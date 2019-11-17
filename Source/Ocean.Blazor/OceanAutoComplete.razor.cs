namespace Oceanware.Ocean.Blazor {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using System.Timers;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.JSInterop;

    /// <summary>
    /// An autocomplete input component.
    /// </summary>
    /// <typeparam name="TItem">The type of the t item.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "ET001:Type name does not match file name", Justification = "Waiting for .NET Core 3.1 RTM")]
    public class OceanAutoCompleteBase<TItem> : ComponentBase, IDisposable {
        const Int32 DownArrowCount = 1;
        const Int32 DownPageCount = 6;
        const Int32 UpArrowCount = -1;
        const Int32 UpPageCount = -6;
        Timer _debounceTimer;
        EditContext _editContext;
        FieldIdentifier _fieldIdentifier;
        String _searchText = String.Empty;
        String _value;

        /// <summary>
        /// Gets or sets the additional attributes added to the element razor markup.
        /// </summary>
        /// <value>The additional attributes.</value>
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<String, Object> AdditionalAttributes { get; set; }

        /// <summary>
        /// Gets or sets the auto complete container CSS class.
        /// </summary>
        /// <value>The auto complete container CSS class.</value>
        [Parameter]
        public String AutoCompleteContainerCssClass { get; set; } = "auto-complete-container";

        /// <summary>
        /// Gets or sets the debounce time between key strokes and the search callback being invoked.  Default is 300ms.
        /// </summary>
        /// <value>The debounce.</value>
        [Parameter]
        public Int32 Debounce { get; set; } = 300;

        /// <summary>
        /// Gets or sets the item string selector.
        /// <para>
        /// The purpose of this function is to return the value to be displayed in the input control upon item selection.
        /// </para>
        /// </summary>
        /// <example>
        /// For search result item has a City property use this:  ItemStringSelector="@(item => item.City)"
        /// </example>
        /// <value>The item string selector.</value>
        [Parameter]
        public Func<TItem, String> ItemStringSelector { get; set; }

        /// <summary>
        /// Gets or sets the item template used to render the search result item.
        /// </summary>
        /// <example>
        /// For a search result that has City, State, and Zip properties and you want them to be displayed use this ItemTemplate:
        ///     <ItemTemplate Context="item">
        ///         @item.City, @item.State @item.Zip
        ///     </ItemTemplate>
        /// </example>
        /// <value>The item template.</value>
        [Parameter]
        public RenderFragment<TItem> ItemTemplate { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the search before the search callback is invoked, the default value is 1..
        /// </summary>
        /// <value>The minimum length of the search.</value>
        [Parameter]
        public Int32 MinimumSearchLength { get; set; } = 1;

        /// <summary>
        /// Gets or sets the template to show when no search results are found.
        /// </summary>
        /// <value>The not found template.</value>
        [Parameter]
        public RenderFragment NotFoundTemplate { get; set; }

        /// <summary>
        /// Gets or sets the search callback.
        /// </summary>
        /// <value>The search callback.</value>
        [Parameter]
        public Func<String, Task<IEnumerable<TItem>>> SearchCallback { get; set; }

        /// <summary>
        /// Gets or sets the selected item changed.
        /// </summary>
        /// <value>The selected item changed.</value>
        [Parameter]
        public EventCallback<TItem> SelectedItemChanged { get; set; }

        /// <summary>
        /// Gets or sets the selected item class. The default value of the selected class is a background-color of whitesmoke.
        /// <para>
        /// To change this value, set this property to the class name you want applied to selected items.
        /// </para>
        /// </summary>
        /// <value>The selected item class.</value>
        [Parameter]
        public String SelectedItemClass { get; set; } = "selected-search-result";

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Parameter]
        public String Value {
            get {
                return _value;
            }
            set {
                _value = value;
                _searchText = value;
            }
        }

        [Parameter]
        public EventCallback<String> ValueChanged { get; set; }

        /// <summary>
        /// Gets or sets the value expression.
        /// </summary>
        /// <value>The value expression.</value>
        [Parameter]
        public Expression<Func<String>> ValueExpression { get; set; }

        /// <summary>
        /// Gets or sets the is searching.
        /// </summary>
        /// <value>The is searching.</value>
        protected Boolean IsSearching { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        protected String SearchText {
            get => _searchText;
            set {
                _searchText = value;

                if (value.Length == 0) {
                    ResetUI();
                } else if (value.Length >= this.MinimumSearchLength) {
                    _debounceTimer.Stop();
                    _debounceTimer.Start();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index of the selected.
        /// </summary>
        /// <value>The index of the selected.</value>
        protected Int32 SelectedIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets the show not found.
        /// </summary>
        /// <value>The show not found.</value>
        protected Boolean ShowNotFound { get; set; }

        /// <summary>
        /// Gets or sets the show suggestions.
        /// </summary>
        /// <value>The show suggestions.</value>
        protected Boolean ShowSuggestions { get; set; }

        /// <summary>
        /// Gets or sets the suggestions.
        /// </summary>
        /// <value>The suggestions.</value>
        protected OceanAutoCompleteItemWrapper<TItem>[] Suggestions { get; set; } = new OceanAutoCompleteItemWrapper<TItem>[0];

        /// <summary>
        /// Gets or sets the cascaded edit context.
        /// </summary>
        /// <value>The cascaded edit context.</value>
        [CascadingParameter]
        EditContext CascadedEditContext { get; set; }

        /// <summary>
        /// Gets or sets the js runtime.
        /// </summary>
        /// <value>The js runtime.</value>
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OceanAutoCompleteBase{TItem}"/> class.
        /// </summary>
        public OceanAutoCompleteBase() {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// <para>
        /// Disposes the debounce timer.
        /// </para>
        /// </summary>
        public void Dispose() {
            _debounceTimer.Dispose();
        }

        /// <summary>
        /// Handles the arrow up and down, page up and down, escape, enter, and tab keys.
        /// <para>
        /// Arrow up and down move the selected item 1 item up or down.
        /// </para>
        /// <para>
        /// Page up and down move the selected item 6 items up or down.
        /// </para>
        /// <para>
        /// Escape closes the search results lists.
        /// </para>
        /// <para>
        /// Enter and tab keys raises the selected item changed event and closes the search results lists.
        /// </para>
        /// </summary>
        /// <param name="args">The KeyboardEventArgs instance containing the event data.</param>
        protected async Task HandleKeyUp(KeyboardEventArgs args) {
            if (Suggestions == null || Suggestions.Length == 0) {
                if (args.Key == "Escape") {
                    ResetUI();
                }
                return;
            }
            if (args.Key == "ArrowDown") {
                await MoveSelection(DownArrowCount);
            } else if (args.Key == "ArrowUp") {
                await MoveSelection(UpArrowCount);
            } else if (args.Key == "PageDown") {
                await MoveSelection(DownPageCount);
            } else if (args.Key == "PageUp") {
                await MoveSelection(UpPageCount);
            } else if (args.Key == "Escape") {
                ResetUI();
            } else if (SelectedIndex >= 0 && SelectedIndex < Suggestions.Count()) {
                if (args.Key == "Enter" || args.Key == "Tab") {
                    await ItemSelected(Suggestions[SelectedIndex].Item);
                }
            }
        }

        /// <summary>
        /// Handles on focus out event.
        /// </summary>
        protected async Task HandleOnFocusOut(FocusEventArgs _) {
            _debounceTimer.Stop();
            if (this.ShowNotFound && this.SelectedIndex == -1) {
                this.Value = _searchText;
                await ValueChanged.InvokeAsync(this.Value);
                _editContext?.NotifyFieldChanged(_fieldIdentifier);
                await SelectedItemChanged.InvokeAsync(default);
                this.ResetUI();
                await InvokeAsync(StateHasChanged);
            } else if (this.ShowSuggestions && this.SelectedIndex > -1) {
                await ItemSelected(Suggestions[SelectedIndex].Item);
            }
        }

        /// <summary>
        /// Invoked when the item is selected.
        /// </summary>
        /// <param name="item">The selected item.</param>
        protected async Task ItemSelected(TItem item) {
            _debounceTimer.Stop();
            await ValueChanged.InvokeAsync(this.ComputeItemStringValue(item));
            await SelectedItemChanged.InvokeAsync(item);
            _editContext?.NotifyFieldChanged(_fieldIdentifier);
            this.ShowSuggestions = false;
            this.SelectedIndex = -1;
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Method invoked when the component is ready to start, having received its
        /// initial parameters from its parent in the render tree.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when SearchCallback is null, but is required. method call is invalid for the object's current state.</exception>
        protected override void OnInitialized() {
            _editContext = CascadedEditContext;
            _fieldIdentifier = FieldIdentifier.Create(ValueExpression);
            _debounceTimer = new Timer();
            _debounceTimer.Interval = Debounce;
            _debounceTimer.AutoReset = false;
            _debounceTimer.Elapsed += Search;

            if (this.SearchCallback == null) {
                throw new InvalidOperationException("SearchCallback is null, but is required.");
            }
            base.OnInitialized();
        }

        /// <summary>
        /// Searches the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        protected async void Search(Object source, ElapsedEventArgs e) {
            IsSearching = true;
            await InvokeAsync(StateHasChanged);

            var searchResults = await SearchCallback?.Invoke(_searchText);
            if (searchResults == null) {
                ResetUI();
                return;
            }

            if (!searchResults.Any()) {
                this.Suggestions = null;
                this.ShowSuggestions = false;
                this.IsSearching = false;
                this.ShowNotFound = true;
                await InvokeAsync(StateHasChanged);
                return;
            }

            var list = new List<OceanAutoCompleteItemWrapper<TItem>>();
            var index = 0;
            foreach (var item in searchResults) {
                list.Add(new OceanAutoCompleteItemWrapper<TItem>(item, IndexToId(index)));
                index++;
            }
            this.ShowNotFound = false;
            this.Suggestions = list.ToArray();
            this.ShowSuggestions = true;
            IsSearching = false;
            await InvokeAsync(StateHasChanged);
        }

        String ComputeItemStringValue(TItem item) {
            return ItemStringSelector?.Invoke(item) ?? item?.ToString();
        }

        /// <summary>
        /// Indexes to identifier.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>String.</returns>
        String IndexToId(Int32 index) {
            const string letters = "ABCDEFGHIJ";
            var indexNumbers = index.ToString();
            var id = string.Empty;
            foreach (var item in indexNumbers) {
                var value = Int32.Parse(item.ToString());
                id = String.Concat(id, letters[value]);
            }
            return id;
        }

        async Task MoveSelection(Int32 count) {
            var index = SelectedIndex + count;

            if (count == DownPageCount || count == UpPageCount) {
                if (index >= Suggestions.Count()) {
                    index = Suggestions.Count() - 1;
                } else if (index < 0) {
                    index = 0;
                }
            } else {
                if (index >= Suggestions.Count()) {
                    return;
                }
                if (index < 0) {
                    return;
                }
            }

            if (this.SelectedIndex > -1) {
                this.Suggestions[this.SelectedIndex].SelectedItemCssClass = "not-selected-search-result"; // String.Empty;
            }

            this.SelectedIndex = index;
            if (this.SelectedIndex > -1) {
                if (String.IsNullOrWhiteSpace(this.SelectedItemClass)) {
                    this.SelectedItemClass = "selected-search-result";
                }
                this.Suggestions[this.SelectedIndex].SelectedItemCssClass = this.SelectedItemClass;
                await Interop.ScrollAutoCompleteIfNeeded(JSRuntime, this.Suggestions[this.SelectedIndex].Id);
            }
        }

        void ResetUI() {
            _debounceTimer.Stop();
            this.ShowSuggestions = false;
            this.ShowNotFound = false;
            this.Suggestions = new OceanAutoCompleteItemWrapper<TItem>[0];
            this.SelectedIndex = -1;
        }
    }
}
