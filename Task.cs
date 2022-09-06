namespace Task3 
{
    class Task 
    {
        public static void GetCountForArray(int[] array, int[] valuesArray) 
        {
            HeapTreeWrapper<int> heap = new HeapTreeWrapper<int>(valuesArray);
            heap.Heapify();
        
            int GetCount(int count, int value) 
            {
                int val = heap.Value;

                if (val <= value) return count + heap.GetCountOfLowerNodes();

                bool left =  heap.TryMoveDownLeft(out val);

                if (!left) 
                {
                    // Не накапливаем, тк не отсеялся ранее
                    return count;
                }
                else 
                {
                    count = GetCount(count, value);
                    heap.TryMoveUp(out val);
                    bool right = heap.TryMoveDownRight(out val);
             
                    if (!right) 
                    {
                        return count;
                    } 
                    else 
                    {
                        count = GetCount(count, value);
                        heap.TryMoveUp(out val);
                    }
                }
                return count;
            }

            for (int i = 0; i < array.Length; i++) 
            {
                heap.ResetHead();
                array[i] = GetCount(0, array[i]);
            }
        }
    }
}