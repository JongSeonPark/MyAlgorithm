#include <iostream>
#include <vector>
using namespace std;

class Node
{
public:
    Node(int value)
    {
        this->value = value;
    }
    int value;
    Node* left = nullptr;
    Node* right = nullptr;
    Node* parent = nullptr;

    bool HasChild()
    {
        return left != nullptr || right != nullptr;
    }
    
    void DetachFromParent()
    {
        if (parent)
        {
            if (parent->left == this)
                parent->left = nullptr;
            else if (parent->right == this)
                parent->right = nullptr;
        }
    }
};

class BinarySearchTree
{
    Node* rootNode = nullptr;
public:
    // 삽입
    void Insert(int value)
    {
        Node* newNode = new Node(value);
        if (rootNode == nullptr)
        {
            rootNode = newNode;
            return;
        }

        Node* tempNode = rootNode;
        while (true)
        {
            if (tempNode->value > value)
            {
                if (tempNode->left == nullptr)
                {
                    tempNode->left = newNode;
                    newNode->parent = tempNode;
                    return;
                }
                else
                {
                    tempNode = tempNode->left;
                }
            }
            else
            {
                if (tempNode->right == nullptr)
                {
                    tempNode->right = newNode;
                    newNode->parent = tempNode;
                    return;
                }
                else
                {
                    tempNode = tempNode->right;
                }
            }
        }
    }

    void Insert(vector<int> values)
    {
        for (int value : values)
        {
            Insert(value);
        }
    }

    Node* Find(int value)
    {
        Node* tempNode = rootNode;

        while (rootNode != nullptr)
        {
            if (tempNode->value == value)
            {
                return tempNode;
            }
            else if (tempNode->value > value)
            {
                tempNode = tempNode->left;
            }
            else
            {
                tempNode = tempNode->right;
            }
        }

        return nullptr;
    }
    
    // 삭제
    void Erase(int value)
    {
        Node* findedNode = Find(value);

        if (findedNode == nullptr) return;

        if (!findedNode->HasChild())
        {
            findedNode->DetachFromParent();
            delete findedNode;
            return;
        }
        else
        {
            if (findedNode->left != nullptr)
            {
                Node* highestNode = FindHighestNode(findedNode->left);
                findedNode->value = highestNode->value;
                highestNode->DetachFromParent();
                delete highestNode;
            }
            else
            {
                Node* lowestNode = FindLowestNode(findedNode->right);
                findedNode->value = lowestNode->value;
                lowestNode->DetachFromParent();
                delete lowestNode;
            }
        }
    }

    Node* FindHighestNode(Node* startNode)
    {
        Node* tempNode = startNode;
        while (tempNode->right != nullptr)
        {
            tempNode = startNode->right;
        }
        return tempNode;
    }

    Node* FindLowestNode(Node* startNode)
    {
        Node* tempNode = startNode;
        while (tempNode->left != nullptr)
        {
            tempNode = startNode->left;
        }
        return tempNode;
    }

    // 전위 순회로 정확한 순서로 카피할 수 있다.
    BinarySearchTree DeepCopy()
    {
        BinarySearchTree bst;
        DeepCopyRecursive(bst, rootNode);
        return bst;
    }

    void TraverseInOrder()
    {
        TraverseInOrderRecursive(rootNode);
    }

    void TraversePreOrder()
    {
        TraversePreOrderRecursive(rootNode);
    }

    void TraversePostOrder()
    {
        TraversePostOrderRecursive(rootNode);
    }

private:
    void TraverseInOrderRecursive(Node* node)
    {
        if (node == nullptr) return;

        TraverseInOrderRecursive(node->left);
        cout << node->value << " ";
        TraverseInOrderRecursive(node->right);
    }

    void TraversePreOrderRecursive(Node* node)
    {
        if (node == nullptr) return;

        cout << node->value << " ";
        TraversePreOrderRecursive(node->left);
        TraversePreOrderRecursive(node->right);
    }

    void TraversePostOrderRecursive(Node* node)
    {
        if (node == nullptr) return;

        TraversePostOrderRecursive(node->left);
        TraversePostOrderRecursive(node->right);
        cout << node->value << " ";
    }

    void DeepCopyRecursive(BinarySearchTree& bst, Node* node)
    {
        if (node == nullptr) return;
        bst.Insert(node->value);
        DeepCopyRecursive(bst, node->left);
        DeepCopyRecursive(bst, node->right);
    }

};

int main()
{
    BinarySearchTree bst;
    bst.Insert({ 6, 4, 10, 2, 5, 8, 15, 1, 3, 7, 9, 12 });
    bst.Erase(10);

    bst.TraversePreOrder();
    cout << endl;
    bst.TraverseInOrder();
    cout << endl;
    bst.TraversePostOrder();
    cout << endl;
    cout << endl;

    BinarySearchTree copyBst = bst.DeepCopy();
    copyBst.TraversePreOrder();
    cout << endl;
    copyBst.TraverseInOrder();
    cout << endl;
    copyBst.TraversePostOrder();
    cout << endl;

}