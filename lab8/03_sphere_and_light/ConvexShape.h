#pragma once
#include "Plane.h"
#include<vector>
class ConvexShape : public CPlane
{
public:
	/*
	Плоскость задается коэффициентами уравления плоскости ax+by+cz+d=0,
	а также матрицей начального преобразования плоскости
	*/
	ConvexShape(double a, double b, double c, double d, std::vector<CVector3d> const& points, CMatrix4d const& transform = CMatrix4d());

	/*
	Нахождение точки пересечения луча с плоскостью
	*/
	virtual bool Hit(CRay const& ray, CIntersection& intersection) const;

protected:

	ConvexShape(CMatrix4d const& transform = CMatrix4d());
	void SetPlaneAndPonts(double a, double b, double c, double d, std::vector<CVector3d> const& points);

	virtual void OnUpdateTransform();

private:
	std::vector<CVector3d> m_points;
};
