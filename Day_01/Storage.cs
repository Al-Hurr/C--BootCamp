
namespace Day_01
{
    public class Storage
    {
        public int GoodsCount { get => _goodsCount;}

        private int _goodsCount;

        public Storage(int goodsCount)
        {
            _goodsCount = goodsCount;
        }

        public void ReduceGoods(int count)
        {
            System.Threading.Interlocked.Add(ref _goodsCount, -count);
        }

        public bool IsEmpty() => GoodsCount < 1;
    }
}
