//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ManageInventory.Service
//{
//    public class ManageInventory
//    {
//        public bool PlaceOrder(string creditCardNumber, long itemId, long quantity)
//        {
//            if (ItemInStock(itemId, quantity))
//            {
//                //get itemId Price
//                var price = GetItemPrice(itemId);
//                var amount = (price * quantity);
//                var isPaymentSucced = ChargePayment(creditCardNumber, amount);
//                if (isPaymentSucced)
//                {
//                    //send email for confirmation
//                }
//                else
//                {
//                    throw new Exception("Payment Failed.");
//                }
//            }
//            else
//            {
//                throw new Exception("Sorry, Item not in Stock.");
//            }
//        }

//        public bool ItemInStock(long itemId, long quantity)
//        {
//            // DB call to check item
//            return true;
//        }

//        public bool ChargePayment(string creditCardNumber, decimal amount)
//        {
//            // call api to get
//            return true;
//        }

//        public decimal GetItemPrice(long itemId)
//        {
//            // DB call to get item price
//            return (decimal)103.45;
//        }
//    }
//}
