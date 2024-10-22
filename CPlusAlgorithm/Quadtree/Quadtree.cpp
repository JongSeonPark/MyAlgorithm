// Quadtree.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <unordered_set>
using namespace std;

float MIN_QUAD_DIMENSION = 2.f;

struct Point
{
	float x, y;
};

class Quadtree
{
public:

	Quadtree(float min_x, float max_x, float min_y, float max_y)
	{
		this->min_x = min_x;
		this->max_x = max_x;
		this->min_y = min_y;
		this->max_y = max_y;
	}
	float min_x;
	float max_x;
	float min_y;
	float max_y;

	int depth = 0;
	unordered_set<Point*> points;

	Quadtree* topLeft = nullptr;
	Quadtree* topRight = nullptr;
	Quadtree* bottomLeft = nullptr;
	Quadtree* bottomRight = nullptr;

	bool IsLeaf()
	{
		return topLeft == nullptr;
	}

	bool Insert(Point* p)
	{
		if (!Contains(*p))
		{
			return false;
		}

		points.insert(p);

		if (!IsLeaf())
		{
			topLeft->Insert(p);
			topRight->Insert(p);
			bottomLeft->Insert(p);
			bottomRight->Insert(p);
		}
		return true;
	}

	bool Contains(Point& p)
	{
		return min_x <= p.x && p.x <= max_x && min_y <= p.y && p.y <= max_y;
	}

private:
	void CreateChildren()
	{
		float width = max_x - min_x;
		float height = max_y - min_y;
		if (width < MIN_QUAD_DIMENSION * 2 || height < MIN_QUAD_DIMENSION * 2)
			return;

		int midX = min_x + width / 2.f;
		int midY = min_y + height / 2.f;

	}
};

int main()
{
}