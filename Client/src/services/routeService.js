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



// tour

export const getAllRouteRoad = async () => {
  try {
      const response = await httpRequest.get('route/road');
      return response.data;
  } catch (error) {
      console.error('Error fetching route');
  }
};


export const addTour = async (tourData) => {
  try {
      const response = await httpRequest.post("/tour", tourData, {
          headers: {
              'Content-Type': 'application/json',
          },
      });
      return response.data; 
  } catch (error) {
      throw error.response ? error.response.data : error.message;
  }
};


export const getTourUpdate = async (detailRouteId) => {
  try {
      const response = await httpRequest.get(`route/detail/${detailRouteId}`);
      return response.data;
  } catch (error) {
      console.error('Error fetching route');
  }
};


export const updateTour = async (detailRouteId, tour) => {
  try {
      const response = await httpRequest.put(`tour/${detailRouteId}`, tour);
      return response.data; 
  } catch (error) {
      console.error('Error updating tour:', error);
      throw new Error(error.response?.data?.message || 'Failed to update tour');
  }
};



export const isCheckBooking = async (detailRouteId) => {
  try {
      const response = await httpRequest.get(`/tour/check-booking/${detailRouteId}`);
      return response.data; 
  } catch (error) {
      throw new Error(error.response?.data || 'Failed to check tour status');
  }
};


export const deleteTour = async (detailRouteId) => {
  try {
      const response = await httpRequest.delete(`/tour/${detailRouteId}`);
      return response.data; 
  } catch (error) {
      throw new Error(error.response?.data || 'Failed to check tour status');
  }
};


export const searhcByDetailRouteId = async (page, size, sort,detailRouteId) => {
  try {
      const response = await httpRequest.get(`/tour/search?page=${page}&size=${size}&sort=${sort}&detailRouteId=${detailRouteId}`);
      return response.data;
  } catch (error) {
      console.error('Error fetching user');
      throw error;
  }
};
