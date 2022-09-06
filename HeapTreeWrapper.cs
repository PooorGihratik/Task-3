using System;

namespace Task 
{
    public class HeapTreeWrapper<Type> where Type : IComparable 
    {
        private Type[] array;
    
        public Type[] Array 
        {
            get 
            {
                return array;
            }
            set 
            {
                if (!(value == null || value.Length == null)) 
                {
                    currentIndex = 0;
                    array = value;
                }
            }
        }
    
        private int currentIndex = -1;

        public Type Value 
        {
            get 
            {
                return array[currentIndex];
            }
            set 
            {
                array[currentIndex] = value;
            }
        }

        private bool checkForArray() 
        {
            return !(array == null || array.Length == 0);
        }

        private static int getDepthLevel(int index) 
        {
            return IntMathUtils.Log2(index + 1); 
        }

        public int GetCountOfLowerNodes() 
        {
            if (currentIndex != -1) 
            {
                // Макс глубина дерева
                int maxDepth = getDepthLevel(array.Length); 
                // Текущая глубина нода
                int currentNodeDepth = getDepthLevel(currentIndex);
                // Количество низлежащих нодов без учета последнего уровня глубины
                int countOfLowerNodesWithoutLastDepth = IntMathUtils.PositivePow(2, maxDepth - currentNodeDepth) - 1;
                // Количество нодов в нижнем уровне
                int countOfNodesOnLast = array.Length - IntMathUtils.PositivePow(2, maxDepth) + 1;
                // Позиция узла на уровне глубины
                int nodePosition = currentIndex - IntMathUtils.PositivePow(2, currentNodeDepth) + 1; 
                // Максимальное число потомков у нода в нижнем уровне
                int maxChildsOnLast = (IntMathUtils.PositivePow(2,maxDepth - currentNodeDepth));
                // Количество в нижнем уровне, которые являются потомками текущего
                int countOfLowerNodesOnLastDepth = 
                    IntMathUtils.Max(
                        IntMathUtils.Min(
                            countOfNodesOnLast - maxChildsOnLast * nodePosition,
                            maxChildsOnLast
                        ), 
                        0
                    );
                int result = countOfLowerNodesWithoutLastDepth + countOfLowerNodesOnLastDepth;
                return result;
            }
            else return 0;
        }

        private bool checkForOutOfDepthLevel(int newIndex, out Type value) 
        {
            if (getDepthLevel(currentIndex) != getDepthLevel(newIndex)) 
            {
                value = default(Type);
                return false;
            }   
            else 
            {
                currentIndex = newIndex;
                value = Value;
                return true;
            }
        }

        public bool TryMoveLeft(out Type value) 
        {
            int newIndex = currentIndex - 1;
            return checkForOutOfDepthLevel(newIndex, out value);
        }

        public bool TryMoveRight(out Type value) 
        {
            int newIndex = currentIndex + 1;
            return checkForOutOfDepthLevel(newIndex, out value);
        }

        private bool checkForIndex(int newIndex, out Type value) 
        {
            if (!checkForArray() || newIndex < 0 || newIndex >= array.Length) 
            {
                value = default(Type);
                return false; 
            }
            else 
            {
                currentIndex = newIndex;
                value = Value;
                return true;
            }
        }

        public bool TryMoveDownLeft(out Type value) 
        {
            int newIndex = currentIndex * 2 + 1;
            return checkForIndex(newIndex, out value);
        }

        public bool TryMoveUp(out Type value) 
        {
            int newIndex = currentIndex / 2 - (1 - currentIndex % 2);
            return checkForIndex(newIndex, out value);
        }

        public bool TryMoveDownRight(out Type value) 
        {
            int newIndex = currentIndex * 2 + 2;
            return checkForIndex(newIndex, out value);
        }

        public void ResetHead() 
        {
            if (checkForArray()) currentIndex = 0;
        }
    
        public HeapTreeWrapper(Type[] array) 
        {
            Array = array;
        }

        // Построение max дерева
        public void Heapify() 
        {
            void heapify(int i) 
            {
                int largest = i;
                int left = 2*i + 1;
                int right = 2*i + 2;
                if (left < array.Length && array[left].CompareTo(array[largest]) > 0) largest = left;
                if (right < array.Length && array[right].CompareTo(array[largest]) > 0) largest = right;
                if (largest != i) 
                {
                    Type swap = array[i];
                    array[i] = array[largest];
                    array[largest] = swap;
                    heapify(largest);
                }
            }
            for (int i = array.Length / 2 - 1; i >= 0; i--) heapify(i);
        }
    }
}