using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BlissApp.BLL.MemberModule;
using BlissApp.Control;
using BlissApp.DTO.MemberModule;
using BlissApp.Pages.Dependants;
using BlissApp.Utility;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BlissApp.ViewModels
{
    public partial class DependantsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;
        public ObservableCollection<MemberDTO> listofDependants { get; set; } = new();

        private readonly IMemberRepository memberRepository;

        public DependantsViewModel(

            IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;

            Title = "";
        }

        [RelayCommand]
        public async Task GetDependants()
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

                var data = (await memberRepository.GetMember()).Item2;

                var list = data.listOfMembers;

                var dependants = list.Where(x => !x.relation.Contains(@"Self")).ToList();

                if (listofDependants.Count() != 0)
                {
                    listofDependants.Clear();
                }
                foreach (var item in dependants)
                {
                    listofDependants.Add(item);
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
        public async Task GetEcardMembers()
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

                var data = (await memberRepository.GetMember()).Item2;

                var list = data.listOfMembers;


                if (listofDependants.Count() != 0)
                {
                    listofDependants.Clear();
                }
                foreach (var item in list)
                {
                    listofDependants.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Debug.WriteLine($"Something went wrong:{ex.Message}");

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
        async Task GoToDetails(MemberDTO memberDTO)
        {
            try
            {
                if (memberDTO == null)
                {
                    return;
                }

                Preferences.Default.Set("FamilyMemberId", memberDTO.auto_id);

                await Shell.Current.GoToAsync(nameof(DependantDetailPage), true, new Dictionary<string, object>
                {
                    {"MemberDTO", memberDTO }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", ex.Message));

                return;
            }
        }
    }
}
