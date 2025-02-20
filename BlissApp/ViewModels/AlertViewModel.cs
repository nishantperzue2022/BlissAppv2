using BlissApp.DTO.MemberModule;
using BlissApp.DTO.Util;
using BlissApp.Utility;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.ViewModels

{
    [QueryProperty("Text","Text")]
    public partial class AlertViewModel : BaseViewModel
    {

        [ObservableProperty]
        string? text;

      
    }
}
