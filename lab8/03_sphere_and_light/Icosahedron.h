#pragma once
#include "GeometryObjectImpl.h"
#include <vector>
#include "Triangle.h"
class Icosahedron : public CGeometryObjectImpl
{
public:
	Icosahedron(CMatrix4d const& transform = CMatrix4d());

	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

private:
	std::vector<Triangle> m_trinagles;
};
