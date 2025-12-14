/*
#include <iostream>
#include <list>
#include <algorithm>
using namespace std;

class List{
	private:
		int *arr;
		int size;
        int cap;
        void expand(){
            cap = cap*2;
            int *tmp = new int[cap];
            for (int i = 0; i<size; i++)
            {
                tmp[i] = arr[i];
            }
            delete arr;
            arr = tmp;
        }
	public:
		List(int s);
        void printArray();
        void append(int val);
        void insert(int val, int pos);
		
};

List::List(int s)
{
    arr = new int[s];
    cap = s;
    size = 0;
}


void List::insert(int val, int pos)
{
    if (pos>size || pos<0)
    {
        cout << "Invalid position" << endl;
    }
    else
    {
        if (size==cap){ expand(); }

        for (int i = size-1; i>=pos; i--)
        {
            arr[i+1]=arr[i];
        }
        arr[pos] = val;
        size++;

        
        
        
        // Other Version
        if (size < cap)
        {
            for (int i = size; i>=pos; i--)
            { arr[i]=arr[i-1];}
            arr[pos]=val;
            size++;
           
        }
        else{
            expand();
            for (int i = size; i>=pos; i--)
            { arr[i]=arr[i-1];}
            arr[pos]=val;
            size++;
        }


    }
    
    
}


void List::append(int val)
{
    if (size==cap) expand();
    arr[size]=val;
    size++;
}


void List::printArray()
{
    for (int i = 0; i<size; i++)
    {
        cout << arr[i] << " ";
    }
    cout << endl;
    cout << "Size = " << size << endl;
    cout << "Cap = " << cap << endl;
    cout << endl;
}


int main()
{
	List L1(10);
    L1.append(11);
    L1.append(13);
    L1.append(15);
    L1.append(17);
    L1.append(22);
    L1.append(11);
    L1.append(13);
    L1.append(15);
    L1.append(17);
    L1.printArray();
    L1.insert(100, 3);
    L1.printArray();
    L1.insert(101, 3);
	L1.printArray();
	
	return 0;
}*/

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#include <iostream>
#include <list>
#include <algorithm>
using namespace std;



template <typename T>
class List{
	private:
		T *arr;
		int size;
        int cap;
        void expand(){
            cap = cap*2;
            T *tmp = new T[cap];
            for (int i = 0; i<size; i++)
            {
                tmp[i] = arr[i];
            }
            delete arr;
            arr = tmp;
        }
	public:
		List(int s);
        void printArray();
        void append(T val);
        void insert(T val, int pos);
		
};

template <typename T>       // Must apply to every function that is created outside of class IF you did not implement it in class
List<T>::List(int s)
{
    arr = new T[s];
    cap = s;
    size = 0;
}

template <typename T>
void List<T>::insert(T val, int pos)
{
    if (pos>size || pos<0)
    {
        cout << "Invalid position" << endl;
    }
    else
    {
        if (size==cap){ expand(); }

        for (int i = size-1; i>=pos; i--)
        {
            arr[i+1]=arr[i];
        }
        arr[pos] = val;
        size++;

        
        
        
        // Other Version
        if (size < cap)
        {
            for (int i = size; i>=pos; i--)
            { arr[i]=arr[i-1];}
            arr[pos]=val;
            size++;
           
        }
        else{
            expand();
            for (int i = size; i>=pos; i--)
            { arr[i]=arr[i-1];}
            arr[pos]=val;
            size++;
        }


    }
    
    
}

template <typename T>
void List<T>::append(T val)
{
    if (size==cap) expand();
    arr[size]=val;
    size++;
}

template <typename T>
void List<T>::printArray()
{
    for (int i = 0; i<size; i++)
    {
        cout << arr[i] << " ";
    }
    cout << endl;
    cout << "Size = " << size << endl;
    cout << "Cap = " << cap << endl;
    cout << endl;
}


int main()
{
	List<int> L1(10);
    L1.append(11);
    L1.append(13);
    L1.append(15);
    L1.append(17);
    L1.append(22);
    L1.append(11);
    L1.append(13);
    L1.append(15);
    L1.append(17);
    L1.printArray();
    L1.insert(100, 3);
    L1.printArray();
    L1.insert(101, 3);
	L1.printArray();
	
	return 0;
}


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
