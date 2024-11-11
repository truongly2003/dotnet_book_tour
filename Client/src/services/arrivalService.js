import httpRequest from "../utils/httpRequest";
export const getAllArrival=async()=>{
    try {
        const response =await httpRequest.get("/Route/arrival")
        return response.data;
    } catch (error) {
        console.error(error)
    }
}