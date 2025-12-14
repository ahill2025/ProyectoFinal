#include <iostream>
#include <list>
using namespace std;



template <typename T>
class List{
	private:
		T *arr;
		int size;
	public:
		List(int cap);
		
};

List::List(int cap)
{
	List array = new List[size];
	for (int i = 0; i<size; i++)
	{
		array[i] = 0;
	}
	for (int i = 0; i<size; i++)
	{
		cout << array[i] << " ";
	}
	cout << endl;
	
	return array;
}

int main()
{
	List L1(10);
	
	
	
	
	
	
	return 0;
}