import httpRequest from "../utils/httpRequest";
export const getAllRoutes = async (page, size,sort) => {
  try {
    const response = await httpRequest.get(`/Route/?page=${page}&size=${size}&sort=${sort}`);
    return response.data;
    
  } catch (error) {
    console.error("Error fetching user");
    throw error;
  }
};
export const getRouteDetailById = async (id) => {
  try {
    const response = await httpRequest.get(`/route/detail/${id}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching route");
  }
};
export const getRouteByAllSearch = async (
  arrivalName,
  departureName,
  timeToDeparture,
  page,
  size,
  sort
) => {
  try {
    const response = await httpRequest.get(
      `/Route/search/body?ArrivalName=${arrivalName}&DepartureName=${departureName}&TimeToDeparture=${timeToDeparture}&page=${page}&size=${size}&sort=${sort}`
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching route");
  }
};
export const getAllRouteByArrivalName = async (
  arrivalName,
  page,
  size,
  sort,
) => {
  try {
    const response = await httpRequest.get(
      `/Route/search/arrivalName?ArrivalName=${arrivalName}&page=${page}&size=${size}&sort=${sort}`
    );
    return response.data;
  } catch (error) {
    console.error("Error fetching route");
  }
};
