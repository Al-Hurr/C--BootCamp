
namespace Day_01
{
    public class Storage
    {
        public int GoodsCount { get; set; }

        public Storage(int goodsCount)
        {
            GoodsCount = goodsCount;
        }

        public bool IsEmpty() => GoodsCount < 1;
    }
}
