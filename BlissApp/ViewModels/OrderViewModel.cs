using BlissApp.BLL.MedicalCenterModule;
using BlissApp.BLL.MedicineModule;
using BlissApp.BLL.OrderModule;
using BlissApp.Control;
using BlissApp.Controls;
using BlissApp.DTO.AuthenticationModule;
using BlissApp.DTO.MedicalCentres;
using BlissApp.DTO.MedicineModule;
using BlissApp.DTO.MemberModule;
using BlissApp.DTO.OrderModule;
using BlissApp.Pages.Pharmacy;
using BlissApp.Utility;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BlissApp.ViewModels
{

    public partial class OrderViewModel : BaseViewModel
    {
        public ObservableCollection<OrderDTO>? ListOfOrders { get; set; } = new();

        public ObservableCollection<MedicineDTO>? Medicines { get; set; } = new();

        public ObservableCollection<Listofmedicalcentre> ListOfMedicalCentre { get; set; } = new();

        private readonly IOrderRepository orderRepository;

        private readonly IMedicalCenteRepository medicalCenteRepository;

        private readonly IMedicineRepository medicineRepository;

    
        
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string? _quantity;

        [ObservableProperty]
        string? _pickUplocation;


        [ObservableProperty]
        string _medicineName;

        [ObservableProperty]
        double _quantityValue;


        private MedicineDTO _selectedDepartment;
        public MedicineDTO SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                SetProperty(ref _selectedDepartment, value);

                if (SelectedDepartment != null)
                {

                    var k = _selectedDepartment;

                    OpenCartPage(_selectedDepartment);

                    //DepartmentId = SelectedDepartment.Id;


                }
            }
        }

        private async void OpenCartPage(MedicineDTO  medicineDTO)
        {


            //var navigationParameter = new Dictionary<string, object>
            //{
            //    { "MedicineDTO", selectedDepartment }
            //};

            //await Shell.Current.GoToAsync(nameof(AddToCartPage), animate: true);

            //await Shell.Current.GoToAsync($"AddToCartPage", navigationParameter);

            //var s = "Order has been successfully submitted";

            //await Shell.Current.GoToAsync($"{nameof(AddToCartPage)}?Text={s}", animate: true);

            var data = new Dictionary<string, object>
            {
                        { "MedicineDTO", medicineDTO }
            };          

            await Shell.Current.GoToAsync($"AddToCartPage", data);






        }

        private string _selectedItem;

        public ObservableCollection<string> Items { get; set; }
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));  // Notify UI of change
                }
            }
        }


        private Listofmedicalcentre _selectedPickup;
        public Listofmedicalcentre SelectedPickup
        {
            get { return _selectedPickup; }
            set
            {
                SetProperty(ref _selectedPickup, value);

                if (SelectedPickup != null)
                {
                    PickUplocation = SelectedPickup.Location;
                }
            }
        }



        public OrderViewModel(IOrderRepository orderRepository,
                              IMedicalCenteRepository medicalCenteRepository, IMedicineRepository medicineRepository)
        {
            this.orderRepository = orderRepository;

            this.medicalCenteRepository = medicalCenteRepository;

            this.medicineRepository = medicineRepository;

            Items = new ObservableCollection<string>
        {
            "Tablets",
                    "capsules",
                    "lozenges",
                    "Grapes",
                    "Bottle",
                    "Pouches",
                    "Vials",
                    "Vials",
                    "Syringes",
                    "Ampoules",
                    "Tubes",
                    "Cartons",
                    "Packets",
                    "Doses / Strips"
        };
        }


        [RelayCommand]
        public async Task CreateOrder()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                var logindetails = Preferences.Get("UserInfo", "0");

                var userDetails = JsonConvert.DeserializeObject<Login>(logindetails);

                //if (AppointmentType == "radiology")
                //{
                //    DepartmentId = 2;
                //}

                //if (MedicalCentreId == 0)
                //{
                //    MedicalCentreHasError = true;

                //    MedicalCentreErrorText = "Please  select Medical Centre";

                //    return;
                //}
                //else
                //{
                //    MedicalCentreHasError = false;

                //    MedicalCentreErrorText = "";
                //}

                //if (string.IsNullOrEmpty(CurrentTime.ToString(@"hh\:mm")))
                //{
                //    PriorityTimeHasError = true;

                //    PriorityTimeErrorText = "Please  select priority time";

                //    return;
                //}
                //else
                //{
                //    PriorityTimeHasError = false;

                //    PriorityTimeErrorText = "";
                //}

                IsBusy = true;

                var order = new List<OrderDTO>()
                {
                     new() {
                         MemberID = Guid.Parse(userDetails.Id),

                         Id = Guid.NewGuid(),

                         OrderDate = DateTime.Now,

                         CreateDate = DateTime.Now,

                         Status =0,

                         MedicineName = MedicineName,

                         PickupLocation = PickUplocation,

                         Quantity = Quantity +" " + SelectedItem,
                     }

                };

                var token = userDetails.Access_token;

                var result = await orderRepository.Create(order, token);

                if (result == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Your order has been added to cart"));

                    //await Shell.Current.GoToAsync(nameof(CartItemsPage), animate: true);            

                    return;
                }
                if (result == false)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request, please try again"));

                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request, please try again"));

                return;
            }

            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task GetListOfMedicines()
        {
            try
            {
                var data = await medicineRepository.GetMedicine();

                if (data.Item1 == true)
                {
                    if (data.Item2 != null)
                    {
                        var list = data.Item2.medicines;

                        if (Medicines?.Count() != 0)
                        {
                            Medicines.Clear();
                        }
                        foreach (var item in list)
                        {
                            Medicines.Add(item);
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to fetch data ,please try again later"));

                return;
            }

        }




        [RelayCommand]
        public async Task GetOrders()
        {

            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var getMyOrdeers = (await orderRepository.GetMyOrders()).Item2.orders;

                if (getMyOrdeers != null)
                {

                    if (ListOfOrders?.Count() != 0)
                    {
                        ListOfOrders.Clear();
                    }
                    foreach (var item in getMyOrdeers)
                    {
                        ListOfOrders.Add(item);
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
        public async Task ClearCart()
        {

            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var response = await orderRepository.DeleteAllOrder();

                if (response == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Item has been successfully removed from your cart"));

                    if (ListOfOrders?.Count() != 0)
                    {
                        ListOfOrders.Clear();
                    }
                }
                else
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable process your request ,please try again later"));

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
        public async Task DeleteOrder(OrderDTO orderDTO)
        {

            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var response = await orderRepository.DeleteOrder(orderDTO);

                if (response == true)
                {
                    await GetOrders();

                    await Application.Current.MainPage?.ShowPopupAsync(new SuccessMessage("Success", "Item has been successfully removed from your cart"));
                    
                }
                else
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable process your request ,please try again later"));

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

        //public async Task<List<MedicineDTO>> GetListOfMedicalCentres()
        //{

        //    var list = (await medicineRepository.GetMedicine()).Item3;




        //    var k = new List<MedicineDTO>()
        //    {
        //                new MedicineDTO() { Name = "Panadol", ID = 100 },
        //                new MedicineDTO() { Name = "Amoxicillin", ID = 12 },
        //                new MedicineDTO() { Name = "Penicillin", ID = 1 },
        //                new MedicineDTO() { Name = "Ciprofloxacin ", ID = 2 },
        //                new MedicineDTO() { Name = "LinkedIn", ID = 3 },
        //                new MedicineDTO() { Name = "Skype", ID = 4 },
        //                new MedicineDTO() { Name = "Azithromycin ", ID = 5 },
        //                new MedicineDTO() { Name = "Doxycycline", ID = 6 },
        //                new MedicineDTO() { Name = "Clindamycin", ID = 7 },
        //                new MedicineDTO() { Name = "Fluoxetine ", ID = 8 },
        //                new MedicineDTO() { Name = "Sertraline ", ID = 9 },
        //                new MedicineDTO() { Name = "Omeprazole ", ID = 10 },
        //                new MedicineDTO() { Name = "Ranitidine ", ID = 11 },
        //                new MedicineDTO() { Name = "Lansoprazole ", ID = 12 },
        //                new MedicineDTO() { Name = "Calcium carbonate ", ID = 13 },
        //    }.ToList();

        //    return k;
        //}

        [RelayCommand]
        public async Task GetMedicalCentres()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var data = (await medicalCenteRepository.GetMedicalcentre());

                if (data.Item1 == false)
                {
                    //await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                    return;
                }
                var list = data.Item2.listOfMedicalCentres.OrderBy(x => x.Location);

                if (ListOfMedicalCentre.Count() != 0)
                {
                    ListOfMedicalCentre.Clear();
                }
                foreach (var item in list)
                {
                    ListOfMedicalCentre.Add(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Info", "There is no visit at the moment,please try again later"));

                return;
            }
            finally
            {
                IsBusy = false;

                IsRefreshing = false;
            }

        }



        [RelayCommand]
        public async Task NavigateToCartItems()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(CartItemsPage), animate: true);

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task NavigateToAddToCart()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                await Shell.Current.GoToAsync(nameof(AddToCartPage), animate: true);

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task SubmitOrder()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                var response = await orderRepository.SubmitOrder();

                if (response == true)
                {

                    var s = "Order has been successfully submitted";

                    await Shell.Current.GoToAsync($"{nameof(SuccessPage)}?Text={s}", animate: true);


                    return;
                }
                if (response == true)
                {
                    await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to process your request ,please try again"));

                    return;
                }
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await Application.Current.MainPage?.ShowPopupAsync(new ErrorMessage("Warning", "Unable to view details ,please try again"));

                return;
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
