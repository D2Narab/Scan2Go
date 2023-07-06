using Scan2Go.Enums.Translations;
using Scan2Go.Mapper.Models.TranslationModels;
using Utility.Bases.EntityBases;

namespace Scan2Go.Mapper.BaseClasses;

public class BaseManager
{
    //protected readonly IUser user;

    public BaseManager(/*IUser user*/)
    {
        //this.user = user;
        //BaseMethods.user = user;
    }

    protected AutoMapper.IMapper Mapper
    {
        get
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Translations, TranslationsModel>();
                cfg.CreateMap<TranslationSearchCriteriaModel, TranslationSearchCriteria>();
                cfg.CreateMap<TranslationsListItem, TranslationsModel>();
                cfg.CreateMap<TranslationsModel, Translations>();
                cfg.CreateMap<TranslationsModel, TranslationsListItem>();
                cfg.CreateMap<ListSourceBase, ListSourceModel<TranslationsModel>>();
            });

            return config.CreateMapper();
        }
    }
}