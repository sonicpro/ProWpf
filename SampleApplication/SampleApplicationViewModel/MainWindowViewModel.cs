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

        public Money NetWorth => person.NetWorth;
    }
}
