using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MenuService
{   
    public IJSRuntime _jsRuntime { get; set; }

    public event Action? OnChange;

    public void NotifyStateChanged()
    {
        if (OnChange!=null) OnChange.Invoke();
    }

    public async Task NotifyStateChangedAsync()
    {
        await _jsRuntime.InvokeAsync<object>("InvokeAsync", new object[] { OnChange.Invoke });
    }

}
