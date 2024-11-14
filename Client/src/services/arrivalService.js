import httpRequest from "../utils/httpRequest";
export const getAllArrival=async()=>{
    try {
        const response =await httpRequest.get("/Arrival")
        return response.data;
    } catch (error) {
        console.error(error)
    }
}
