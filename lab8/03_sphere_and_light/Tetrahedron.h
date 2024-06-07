#pragma once
#include "GeometryObjectImpl.h"
#include "Triangle.h"
#include <vector>
class Tetrahedron : public CGeometryObjectImpl
{
public:
	Tetrahedron(
		CVector3d const& A,
		CVector3d const& B,
		CVector3d const& C,
		CVector3d const& D,
		CMatrix4d const& transform = CMatrix4d());


	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

private:
	std::vector<Triangle> m_trinagles;
};
