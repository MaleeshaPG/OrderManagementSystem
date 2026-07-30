namespace OrderManagementSystem.Data
{
    public class Enum
    {
        public enum EmployeeStatus
        {
            Active = 0,
            Inactive = 1,
        }

        public enum RecordDeleteStatus
        {
            Active = 0,
            Deleted = 1,
        }

        public enum RecordStatus
        {
            Active = 0,
            Deleted = 1,
        }

        public enum BaseUnit
        {
            Milliliter = 0,
            Meter = 2,
            Centimeter = 3,
            Inch = 4,
            Foot = 5,
            Yard = 6,
            Packet = 7,
            Box = 8,
            Dozen = 9,
            bottle = 10,
            Piece = 11,
        }

        public enum Unit
        {
          
            Piece = 0,
            kg = 1,
            g = 2,
            Litre = 3,
        }

        public enum  LeadTimeType
        {
            Day = 0,
            Week = 1,
            Month = 2,
            Year = 3,
        }

        public enum OrderStatus
        {
            Placed = 0,
            Accepted = 1,
            Completed =2,
        }


    }
}
