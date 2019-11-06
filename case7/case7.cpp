// case7.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include <iostream>

using namespace std;

int main()
{
	setlocale(0, "");
	int edin;
	double m;
  cout << "Введите единицу массы: ";
  cin >> edin;
  cout << "Введите массу: ";
  cin >> m;
  switch (edin)
  {
  case 1:
	  cout << m;
	  break;
  case 2:
	  cout << m / 100000;
		  break;
  case 3:
	  cout << m / 1000;
	  break;
  case 4:
	  cout << m * 1000;
	  break;
  case 5:
	  cout << (m * 100,"kg");
	  break;
  }
}

