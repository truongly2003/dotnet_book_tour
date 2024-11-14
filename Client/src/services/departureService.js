import httpRequest from "../utils/httpRequest";
export const getAllDeparture = async () => {
  try {
    const response = await httpRequest.get("/Departure");
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
