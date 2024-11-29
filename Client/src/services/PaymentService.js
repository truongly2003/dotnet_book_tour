import httpRequest from "../utils/httpRequest";

export const getAllPaymentMethods = async () => {
  try {
    const response = await httpRequest.get(`/Payment/get-all`);
    return response.data;
  } catch (error) {
    console.error("Error fetching route");
  }
}