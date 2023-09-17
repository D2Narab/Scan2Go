using Scan2Go.Entity.Users;
using Scan2Go.Enums.Translations;
using Scan2Go.Mapper.Models.TranslationModels;
using Scan2Go.Mapper.Models.UserModels;
using Utility.Bases;
using Utility.Bases.EntityBases;

namespace Scan2Go.Mapper.BaseClasses;

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
                cfg.CreateMap<ListSourceBase, ListSourceModel<TranslationsModel>>();
                cfg.CreateMap<Translations, TranslationsModel>();
                cfg.CreateMap<TranslationSearchCriteriaModel, TranslationSearchCriteria>();
                cfg.CreateMap<TranslationsListItem, TranslationsModel>();
                cfg.CreateMap<TranslationsModel, Translations>();
                cfg.CreateMap<TranslationsModel, TranslationsListItem>();
                cfg.CreateMap<Users, UsersModel>();
                cfg.CreateMap<UsersModel, Users>();
                cfg.CreateMap<UsersModel, UserListItem>();
                cfg.CreateMap<UserListItem, UserListItemModel>();
                cfg.CreateMap<UserListItem, UsersModel>();
                cfg.CreateMap<UsersSearchCriteriaModel, UsersSearchCriteria>();
                cfg.CreateMap<ListSourceBase, ListSourceModel<UsersModel>>();
                cfg.CreateMap<ListSourceBase, ListSourceModel<UserListItemModel>>();
                cfg.CreateMap<ListItemBase, ListItemModelBase<UserListItemModel>>();
            });

            return config.CreateMapper();
        }
    }
}