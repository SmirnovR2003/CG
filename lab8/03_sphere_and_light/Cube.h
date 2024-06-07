#pragma once
#include "GeometryObjectImpl.h"
#include "Plane.h"
#include "Intersection.h"
#include "ConvexShape.h"
class Cube : public CGeometryObjectImpl
{
public:
	Cube(CMatrix4d const& transform = CMatrix4d());
	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

private:

	ConvexShape m_minusX;
	ConvexShape m_minusY;
	ConvexShape m_minusZ;
	ConvexShape m_plusX;
	ConvexShape m_plusY;
	ConvexShape m_plusZ;
};
