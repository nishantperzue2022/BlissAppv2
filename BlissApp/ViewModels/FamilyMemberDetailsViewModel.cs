using BlissApp.BLL.FamilyMemberModule;
using BlissApp.Control;
using BlissApp.DTO.MemberModule;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{
    public partial class FamilyMemberDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<FamilyMemberDTO> listOfFamilyMember { get; set; } = new();

        private readonly IFamilyMemberRepository familyMemberRepository;
        public FamilyMemberDetailsViewModel(IFamilyMemberRepository familyMemberRepository)
        {
            this.familyMemberRepository = familyMemberRepository;
        }

        [RelayCommand]
        public async Task GetFamilyMember()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;

                //await TokenValidator.CheckTokenValidity();

                var data = (await familyMemberRepository.GetFamilyMember()).Item2;

                if (data != null)
                {
                    var list = data.ListOfMembers.ToList();

                    if (listOfFamilyMember.Count != 0)
                    {
                        listOfFamilyMember.Clear();
                    }
                    foreach (var item in list)
                    {
                        listOfFamilyMember.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }

        [RelayCommand]
        public async Task EditDetails(FamilyMemberDTO familyMemberDTO)
        {
            try
            {
                if (familyMemberDTO == null)
                {
                    return;
                }

                var data = new Dictionary<string, object>
                    {
                        { "FamilyMemberDTO", familyMemberDTO }
                    };

                Preferences.Default.Set("FamilyMemberId", familyMemberDTO.Id);

                await Shell.Current.GoToAsync($"EditMemberDetailsPage", data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
        }


        [RelayCommand]
        public async Task Delete(FamilyMemberDTO familyMemberDTO)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                //if (connectivity.NetworkAccess != NetworkAccess.Internet)
                //{
                //    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("No Internet!", "Please check your internet connectivity and try again"));

                //    return;
                //}

                IsBusy = true;

                await TokenValidator.CheckTokenValidity();

                var result = await familyMemberRepository.DeleteMember(familyMemberDTO);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Record has been successfully deleted"));

                    IsBusy = false;

                    IsRefreshing = true;

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request at this time, please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request at this time, please try again"));

                return;
            }

            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }
    }
}
