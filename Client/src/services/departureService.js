import httpRequest from "../utils/httpRequest";
export const getAllDeparture = async () => {
  try {
    const response = await httpRequest.get("/Route/departure");
    return response.data;
  } catch (error) {
    console.error(error);
  }
};
