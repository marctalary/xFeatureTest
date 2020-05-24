namespace ExampleTestProject.BasicExample
{
    public class BasicSubjectUnderTest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
