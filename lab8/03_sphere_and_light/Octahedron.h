#pragma once
#include "GeometryObjectImpl.h"
#include <vector>
#include "Triangle.h"
class Octahedron : public CGeometryObjectImpl
{
public:
	Octahedron(CMatrix4d const& transform = CMatrix4d());

	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

private:
	std::vector<Triangle> m_trinagles;
};
