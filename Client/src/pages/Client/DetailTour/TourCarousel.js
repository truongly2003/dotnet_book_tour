import { useState,useEffect } from "react";
import Carousel from "react-bootstrap/Carousel";
function TourCarousel({ detailRoute }) {

  const [activeImage,setActiveImage]=useState(0)
  const intervalTime = 2000; 
  useEffect(()=>{
    const interval=setInterval(()=>{
      setActiveImage((prev)=>
        prev===detailRoute.imageList.length -1 ? 0 : prev +1
      );
    },intervalTime);
    return ()=>clearInterval(interval)
  },[detailRoute.imageList.length])
  const handleThumbnailClick=(index)=>{
    setActiveImage(index)
  }
  return (
    <div className="">
      <div className="position-relative">
        <Carousel activeIndex={activeImage} interval={intervalTime}>
          {detailRoute.imageList.map((item, index) => (
            <Carousel.Item key={index}>
              <img
                className="d-block w-100"
                src={require(`../../../assets/images/Tour/${item.textImage}`)}
                alt={item.textImage}
                style={{ height: "400px", objectFit: "cover" }}
              />
            </Carousel.Item>
          ))}
        </Carousel>
        <div className="d-flex mt-2"  style={{
            overflowX: "auto", 
            whiteSpace: "nowrap", 
          }}>
          {detailRoute.imageList.map((item,index) => (
            <img
              onClick={()=>handleThumbnailClick(index)}
              key={index}
              src={require(`../../../assets/images/Tour/${item.textImage}`)}
              alt={item.textImage}
              className="img-thumbnail me-2"
              style={{ width: "80px", height: "60px", objectFit: "cover",cursor: "pointer"  }}
            />
          ))}
        </div>
      </div>
    </div>
  );
}

export default TourCarousel;
