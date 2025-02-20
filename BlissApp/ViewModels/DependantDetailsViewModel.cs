using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.Control;
using BlissApp.DTO.MemberModule;
using BlissApp.Pages.Dependants;
using BlissApp.Utility;
using System.Diagnostics;

namespace BlissApp.ViewModels
{
    [QueryProperty(nameof(Auto_id),nameof(Auto_id))]
    public partial class DependantDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        MemberDTO memberDTO;

        [ObservableProperty]
        public int _auto_id;

        [RelayCommand]
        public async Task SendDeleteRequest()
        {
            try
            {
                await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success!", "Your requst has been sent successfully !"));

                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Please enter member number !"));

                return;
            }
        }

        [RelayCommand]
        public async Task UploadECardPhoto(MemberDTO memberDTO)
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(UploadEcardPhotoPage));

                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Please enter member number !"));

                return;
            }
        }

        [RelayCommand]
        public async Task BackToDependants()
        {
            await Shell.Current.GoToAsync("..");

        }
    }
}
