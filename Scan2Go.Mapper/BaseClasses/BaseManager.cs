using Utility.Bases;

namespace Scan2Go.Mapper.BaseClasses
{
    public class BaseManager
    {
        protected readonly IUser user;

        public BaseManager(IUser user)
        {
            this.user = user;
            BaseMethods.user = user;
        }

        protected AutoMapper.IMapper Mapper
        {
            get
            {
                var config = new AutoMapper.MapperConfiguration(cfg =>
                {

                });

                return config.CreateMapper();
            }
        }
    }
}