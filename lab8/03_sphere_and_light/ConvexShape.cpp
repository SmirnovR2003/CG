#include "stdafx.h"
#include "ConvexShape.h"
#include "Intersection.h"
#include "Ray.h"
#include "VectorMath.h"
#include<exception>

ConvexShape::ConvexShape(double a, double b, double c, double d, std::vector<CVector3d> const& points, CMatrix4d const& transform)
	: CPlane(a,b,c,d,transform)
	, m_points(points)
{
	
	if (m_points.size() < 3)
		throw std::exception("ConvexShape must have >=3 points");
}



bool ConvexShape::Hit(CRay const& ray, CIntersection& intersection) const
{
	intersection.Clear();
	CIntersection intersectionTmp;
	if (CPlane::Hit(ray, intersectionTmp))
	{
		CHitInfo hit = intersectionTmp.GetHit(0);

		double tendency;
		CVector3d normal;
		{
			CVector3d A = m_points[0];
			CVector3d B = m_points[1];
			CVector3d C = m_points[2];
			CVector3d AB = B - A;
			CVector3d AHit = hit.GetHitPointInObjectSpace() - A;

			normal = Normalize(Cross(AB, AHit));

			double dot = Dot(AB, AHit);
			tendency = dot / fabs(dot);
		}


		for (int i = 0; i < m_points.size(); i++)
		{
			CVector3d A = m_points[i % m_points.size()];
			CVector3d B = m_points[(i + 1) % m_points.size()];
			CVector3d AB = B - A;
			CVector3d AHit = hit.GetHitPointInObjectSpace() - A;
			double dot = Dot(AB, AHit);

			if (dot == 0)
				continue;
			auto n = Normalize(Cross(AB, AHit));
			if (!((fabs(normal.x - n.x) <= 1e-4) 
				&& (fabs(normal.y - n.y) <= 1e-4) 
				&& (fabs(normal.z - n.z) <= 1e-4)))
				return false;
			

			/*if (dot != 0 && (dot / fabs(dot) != tendency))
				return false;*/
		}

		hit = CHitInfo(
			hit.GetHitTime(), // Когда столкнулись
			*this, // С кем
			hit.GetHitPoint(), hit.GetHitPointInObjectSpace(), // Точка соударения луча с поверхностью
			hit.GetNormal(), hit.GetNormalInObjectSpace() // Нормаль к поверхности в точке соударения
		);
		intersection.AddHit(hit);
		return true;
	}

	return false;
}

ConvexShape::ConvexShape(CMatrix4d const& transform)
	: CPlane(transform)
{
}

void ConvexShape::SetPlaneAndPonts(double a, double b, double c, double d, std::vector<CVector3d> const& points)
{
	CPlane::SetPlane(CVector4d(a, b, c, d));
	m_points = points;
	if (m_points.size() < 3)
		throw std::exception("ConvexShape must have >=3 points");
}

void ConvexShape::OnUpdateTransform()
{
	
}
