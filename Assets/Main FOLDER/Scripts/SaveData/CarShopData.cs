using System.Collections.Generic;

namespace SaveData
{
    [System.Serializable]
    public class CarShop
    {
        public bool[] AllCars;
        public int checkCar;

        //Заполняем список нужным значением
        public void FillCarsList(int count)
        {
            AllCars = new bool[count];
            AllCars[0] = true;
        }
        
        //Устанавливаем значение конкретному ID
        public bool SetCarList(int id, bool isWhat)
        {
            return AllCars[id] = isWhat;
        }

        //Берем значение у конкретного ID
        public bool GetCarList(int id)
        {
            return AllCars[id];
        }

        //Чистка списка
        public void CleadAllCars()
        {
            if (AllCars.Length > 0)
            {
                for (int i = 0; i < AllCars.Length; i++)
                {
                    AllCars[i] = false;
                } 
            }
        }
    }
}
