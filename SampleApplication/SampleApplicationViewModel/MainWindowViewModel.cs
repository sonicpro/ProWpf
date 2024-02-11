using SampleApplicationModel;

namespace SampleApplicationViewModel
{
    public class MainWindowViewModel
    {
        private IPerson person;

        public MainWindowViewModel(IPerson person)
        {
            this.person = person;
        }

        public MoneyViewModel NetWorth => new MoneyViewModel(person.NetWorth);
    }
}
