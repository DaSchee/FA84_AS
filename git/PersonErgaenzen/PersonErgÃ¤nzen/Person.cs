using System;
namespace PersonErgänzen
{
    public class Person
    {
        private string name;
        private DateTime birthday;
                
        public Person(string name)
        {
            this.Name = name;
        }
        public Person() { }

        public Person(string name, DateTime birthday)
        {
            this.name = name;
            this.birthday = birthday;
        }

        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        }

        public DateTime Birthday
        {
            set { this.birthday = value; }
            get { return this.birthday; }
        }

        public bool IsAtleast(int years)
        {
            int age = this.GetAge();
            return age >= years;
        }

        public int GetAge()
        {
            int age;
            age = DateTime.Now.Year - birthday.Year;
            if (DateTime.Now.DayOfYear < birthday.DayOfYear)
                age = age - 1;

            return age;
        }
    }
}
