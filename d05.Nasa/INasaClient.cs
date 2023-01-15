
namespace d05.Nasa
{
    interface INasaClient<in TIn, out TOut>
    {
        TOut GetAsync(TIn input);
    }
}
