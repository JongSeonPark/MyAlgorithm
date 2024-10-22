// Trie.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <map>
#include <unordered_map>
#include <vector>
using namespace std;

class Node
{
public:
	Node()
	{
		isRoot = true;
	}

	Node(char value)
	{
		this->value = value;
	}

	~Node()
	{
		for (auto iter : childs)
		{
			delete iter.second;
		}
	}

	char value;
	bool isRoot = false;
	bool isWord = false;

	Node* parent;
	unordered_map<char, Node*> childs;

	string GetWord()
	{
		string result;
		Node* tempNode = this; 
		while (tempNode != nullptr && !tempNode->isRoot)
		{
			result = tempNode->value + result;
			tempNode = tempNode->parent;
		}
		return result;
	}
};

class Trie
{
	Node* rootNode;
public:
	Trie()
	{
		rootNode = new Node();
	}

	~Trie()
	{
		delete rootNode;
	}

	void Insert(string word)
	{
		Node* tempNode = rootNode;
		for (int i = 0; i < word.size(); i++)
		{
			if (tempNode->childs.find(word[i]) == tempNode->childs.end())
			{
				Node* newNode = new Node(word[i]);
				newNode->parent = tempNode;
				tempNode->childs.insert({ word[i], newNode });
			}
			tempNode = tempNode->childs[word[i]];
		}

		if (tempNode != rootNode)
		{
			tempNode->isWord = true;
		}
	}

	void Insert(vector<string> words)
	{
		for (auto word : words)
		{
			Insert(word);
		}
	}

	vector<string> GetDictionary(string prefix)
	{
		vector<string> result;
		Node* tempNode = rootNode;
		for (int i = 0; i < prefix.size(); i++)
		{
			if (tempNode->childs.find(prefix[i]) == tempNode->childs.end())
			{
				return result;
			}
			tempNode = tempNode->childs[prefix[i]];
		}

		FindWordRecursive(result, tempNode);

		return result;
	}

private:
	void FindWordRecursive(vector<string>& result, Node* node)
	{
		if (node->isWord)
		{
			result.push_back(node->GetWord());
		}
		for (auto n : node->childs)
		{
			FindWordRecursive(result, n.second);
		}
	}
};

int main()
{
	Trie trie;

	trie.Insert(vector<string>({
		"a",
		"aah",
		"aback",
		"abacus",
		"abandon",
		"able",
		"ably",
		"b",
		"ba",
		"babble",
		}));

	auto words = trie.GetDictionary("aba");

	for (string word : words)
	{
		cout << word << endl;
	}
}