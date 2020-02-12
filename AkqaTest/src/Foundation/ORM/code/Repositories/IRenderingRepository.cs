using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace AkqaTest.Foundation.ORM.Repositories
{
    public interface IRenderingRepository
    {
        T GetDataSourceItem<T>(GetKnownOptions options) where T : class;
        T GetDataSourceItem<T>() where T : class;       

        bool HasDataSource { get; }
        Item DataSourceItem { get; }
        string RenderingParameters { get; }
    }
}
