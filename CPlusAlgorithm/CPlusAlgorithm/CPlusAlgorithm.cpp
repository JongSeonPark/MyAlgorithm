// Test_C++.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <vector>
#include <string>
#include <queue>
#include <set>
#include <algorithm>
#include <bitset>
#include <stack>
#include <cmath>

using namespace std;

#pragma region Vector

bool CompareGreater(const string& A, const string& B)
{
	return A > B;
}

void PrintVector(const vector<string> vec, string Name = "")
{
	if (Name != "")
	{
		cout << Name << ": ";
	}
	for (int i = 0; i < vec.size(); i++)
	{
		cout << vec[i] << " ";
	}
	cout << endl;
}

void TestVector()
{
	vector<string> players({ "BB", "AA", "DD", "CC" });
	vector<string> answers(players);
	PrintVector(players, "초기값");

	// 삽입 삭제
	players.push_back("BA");
	players.push_back("BA");
	players.push_back("BA");
	players.pop_back();
	PrintVector(players, "삽입 삭제");

	// Insert
	auto iter = players.begin();
	players.insert(players.begin(), "A");
	players.insert(players.begin(), 2, "A");
	// players.insert(players.begin(), players.begin() + 3);
	// find Algorithm
	vector<string>::iterator iter2 = find(players.begin(), players.end(), "BB");
	// cout << *iter2 << endl;
	players.insert(iter2, "A");
	PrintVector(players, "Insert");

	// 정렬
	// sort(players.begin(), players.end(), greater<string>());
	sort(players.begin(), players.end(), CompareGreater);
	PrintVector(players, "Sort");

	// index 찾기
	vector<string>::iterator iter3 = find(players.begin(), players.end(), "BB");
	int A = iter3 - players.begin();
	cout << "Index: " << A << endl;

	// 삭제
	// players.erase(iter2);
	// players.erase(iter, iter2);

	// Swap
	swap(players[0], players[1]);
	PrintVector(players, "Swap");

	// Unique
	auto UniqueIter = unique(players.begin(), players.end());

	PrintVector(players, "uniq");
	cout << "UniqueIndex: " << UniqueIter - players.begin() << endl;

	pair<int, int> pair1 = make_pair(3, 3);

}

#pragma endregion

#pragma region Queue

void TestPriorityQueue()
{
	// priority_queue<int> priorityQueue;
	priority_queue<int, vector<int>, greater<int>> priorityQueue;
	priorityQueue.push(3);
	priorityQueue.push(5);
	priorityQueue.push(4);

	while (!priorityQueue.empty())
	{
		cout << "Top: " << priorityQueue.top() << endl;
		priorityQueue.pop();
	}
}

void TestQueue()
{
	queue<int> queue;
	queue.push(3);
	queue.push(5);
	queue.push(4);

	while (!queue.empty())
	{
		cout << "Front: " << queue.front() << ", Back: " << queue.back() << endl;
		queue.pop();
	}
}

void TestStack()
{
	stack<int> stack;
	stack.push(3);
	stack.push(5);
	stack.push(4);

	while (!stack.empty())
	{
		cout << "Top: " << stack.top() << endl;
		stack.pop();
	}
}

#pragma endregion

#pragma region Set

struct setTeststruct
{
	setTeststruct(int size)
	{
		this->size = size;
	}

	int size;

	bool operator ==(const setTeststruct& l2) const
	{
		return this->size == l2.size;
	}
};

struct DataComp
{
	bool operator() (const setTeststruct& lhs, const setTeststruct& rhs) const
	{
		return lhs.size == rhs.size;
	}
};

void SetTest()
{
	set<int> s;
	auto iter = s.insert(5);
	auto t = s.find(5);

	set<setTeststruct, DataComp> sTest;

	setTeststruct A(1);
	sTest.insert(A);
	// cout << sTest.size() << endl;


	multiset<int> ms;
}
#pragma endregion

#pragma region string
void stringTest()
{
	string str = "2021.05.02";
	int idx = str.find('.');
	// cout << idx;
	// cout << str.substr(0, idx);

	// 변환
	int y = stoi(str.substr(0, idx));
	cout << y;
	cout << to_string(y);

	string strB = "AAAasdkjasdkj";

	strB.insert(strB.end(), 'A', 5);
	strB.erase(strB.end() - 2);
	cout << strB << endl;

	if (strB.find("asd") == string::npos)
	{
		cout << "No!" << endl;
	}
	else
	{
		cout << strB.find("affsd") << endl;

	}
	// find

	// 뒤집기
	reverse(str.begin(), str.end());


}
#pragma endregion

#pragma region bitset

void bitsetTest()
{
	int integer = 100;
	string str = "101101";
	bitset<16> bitset1(integer);
	bitset<16> bitset2(str);

	cout << bitset1.to_string() << endl;
	cout << bitset1.to_ulong() << endl;

}

#pragma endregion


int main()
{
	// TestVector();
	// stringTest();
	// bitsetTest();
	// SetTest();

	TestPriorityQueue();
	TestQueue();
}