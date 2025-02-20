using System.ComponentModel;

namespace BlissApp
{
    public class MapLocation : INotifyPropertyChanged
    {
        Location _position;

        public string Address { get; }
        public string Description { get; }

        public Location Position
        {
            get => _position;
            set
            {
                _position = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Position)));
            }
        }

        public MapLocation(string address, string description, Location position)
        {
            Address = address;
            Description = description;
            Position = position;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
