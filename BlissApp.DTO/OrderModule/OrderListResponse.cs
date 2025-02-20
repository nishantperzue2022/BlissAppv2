using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlissApp.DTO.OrderModule
{
    public class OrderListResponse
    {

        public OrderDTO[] orders { get; set; }
        public int status { get; set; }

        //public class Order
        //{
        //    public string id { get; set; }
        //    public string memberID { get; set; }
        //    public DateTime orderDate { get; set; }
        //    public string medicineName { get; set; }
        //    public string quantity { get; set; }
        //    public int status { get; set; }
        //    public DateTime createDate { get; set; }
        //}
    }
}
