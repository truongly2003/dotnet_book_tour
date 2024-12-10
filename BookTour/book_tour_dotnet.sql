-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th12 09, 2024 lúc 05:07 AM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `book_tour_dotnet`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `arrivals`
--

CREATE TABLE `arrivals` (
  `arrival_id` int(11) NOT NULL,
  `arrival_name` varchar(255) DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `arrivals`
--

INSERT INTO `arrivals` (`arrival_id`, `arrival_name`, `status_id`) VALUES
(1, 'Bangkok - Pattaya - Công Viên Khủng Long', 1),
(2, 'Lệ Giang - Đại Lý - Shangrila', 1),
(3, 'Nghi Xương - Trương Gia Giới - Thiên Môn Sơn - Phượng Hoàng Cổ Trấn', 1),
(4, 'Nghệ An', 1),
(5, 'Nha Trang', 1),
(6, 'Hà Nội', 1),
(7, 'Bình Thuận', 1),
(8, 'Khánh Hòa', 1),
(9, 'Long An', 1),
(10, 'Thanh Hóa', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `bookings`
--

CREATE TABLE `bookings` (
  `booking_id` int(11) NOT NULL,
  `customer_id` int(11) DEFAULT NULL,
  `total_price` decimal(10,2) DEFAULT NULL,
  `time_to_order` datetime DEFAULT NULL,
  `payment_id` int(11) DEFAULT NULL,
  `payment_status_id` int(11) DEFAULT NULL,
  `detail_route_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `bookings`
--

INSERT INTO `bookings` (`booking_id`, `customer_id`, `total_price`, `time_to_order`, `payment_id`, `payment_status_id`, `detail_route_id`) VALUES
(17, 111, 0.00, '2024-11-27 05:54:23', 1, 1, 9),
(18, 112, 18000000.00, '2024-11-27 05:57:16', 1, 1, 3),
(19, 114, 20000.00, '2024-11-27 06:04:56', 1, 1, 6),
(20, 115, 11500000.00, '2024-11-29 16:32:39', 1, 1, 5),
(21, 126, 15000000.00, '2024-11-30 04:15:08', 1, 1, 3),
(22, 127, 10000000.00, '2024-11-30 04:21:27', 1, 1, 3),
(23, 128, 5000000.00, '2024-11-30 04:28:19', 1, 1, 5),
(24, 129, 10000000.00, '2024-11-30 04:35:26', 1, 1, 3),
(25, 130, 10000000.00, '2024-11-30 04:37:08', 1, 2, 3),
(26, 132, 11500000.00, '2024-11-30 04:42:48', 1, 1, 5),
(27, 133, 9000000.00, '2024-11-30 04:57:32', 1, 2, 5),
(29, 135, 20000000.00, '2024-11-30 05:29:41', 1, 1, 3),
(30, 136, 23000000.00, '2024-11-30 05:39:51', 1, 2, 3),
(31, 138, 1000000.00, '2024-11-30 09:07:32', 1, 1, 6),
(32, 139, 4230000.00, '2024-11-30 09:14:57', 1, 2, 4),
(33, 155, 9000000.00, '2024-12-09 10:45:58', 1, 1, 1),
(34, 156, 18000000.00, '2024-12-09 10:57:57', 1, 2, 3),
(35, 157, 1500000.00, '2024-12-09 11:00:34', 1, 3, 2);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customers`
--

CREATE TABLE `customers` (
  `customer_id` int(11) NOT NULL,
  `customer_name` varchar(255) NOT NULL,
  `customer_email` varchar(255) NOT NULL,
  `customer_address` varchar(255) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `customer_phone` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `customers`
--

INSERT INTO `customers` (`customer_id`, `customer_name`, `customer_email`, `customer_address`, `user_id`, `customer_phone`) VALUES
(107, 'Nguyễn Hoàng Tuấn', 'nguyenhoangtuan12102003@gmail.com', '119/30 Nguyen van Cu', 114, '0827415586'),
(109, 'hoangtuan', 'hoangtuan@gmail.com', 'Default', 116, '0827415586'),
(110, 'test22', 'test22@gmail.com', 'Default', 117, '0827415586'),
(111, 'Tuan Nguyen', 'nguyenhoangtuan1210200@gmail.com', 'abc', 114, '0827415887'),
(112, 'Tuan Nguyen', 'nguyenhoangtuan1102003@gmail.com', 'abc', 114, '0827415887'),
(114, 'Tuan Nguyen', 'nguyenhoangtuan1210003@gmail.com', 'abc', 114, '0827415887'),
(115, 'Tuan Nguyen', 'nguyenhoangtuan2102003@gmail.com', 'abc', 114, '0827415887'),
(116, 'tuannguyen', 'tuannguyen@gmail.com', 'Default', 123, '0827415586'),
(117, 'lytruong', 'lytruong@gmail.com', 'Default', 124, '0827415586'),
(118, 'trinhtruong', 'trinhtruong@gmail.com', 'Default', 125, '0827415586'),
(124, 'Tuấn Nguyễn', 'tuannguyenit2003@gmail.com', '119/30 Nguyen van Cu', 131, '0827415586'),
(126, 'Tuan Nguyen', 'a@gmail.com', 'Sài Gòn ', 131, '0827415887'),
(127, 'Tuan Nguyen', 'nguyenhoanaagtuan12102003@gmail.com', 'Sài Gòn ', 131, '0989876734'),
(128, 'Tuan Nguyena', 'ngzzzuyenhoangtuan12102003@gmail.com', 'Sài Gòn ', 131, '0989876373'),
(129, 'Nguyen Hoang Tuan', 'nguyenahoangtuan12102003@gmail.com', 'Sài Gòn ', 131, '0388787835'),
(130, 'Vu Ngoc Tu', 'nguyenhoang12102003@gmail.com', 'Sài Gòn ', 118, '0388475623'),
(131, '0552_Vũ Ngọc Tú', 'vungoctu12a3@gmail.com', '119/30 Nguyen van Cu', 133, '0827415586'),
(132, 'Tuan Nguyen', 'nguyenhoangtuan102003@gmail.com', 'Sài Gòn ', 133, '0827415887'),
(133, 'Tuan Nguyen', 'nguyenhoangtuan12102@gmail.com', 'Sài Gòn ', 134, '0827415586'),
(135, 'Tuan Nguyen', 'nguyenhoanagtuaaan12102003@gmail.com', 'Sài Gòn ', 131, '0989876464'),
(136, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 'Sài Gòn ', 131, '0827415586'),
(137, 'hoangtuan123', 'hoangtuan123@gmail.com', 'Default', 136, '0827415586'),
(138, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 'a', 131, '0989898786'),
(139, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 'Sài Gòn ', 139, '0898474645'),
(140, 'test11', 'test11@gmail.com', 'Default', 142, '0827415586'),
(141, 'test12345', 'test12345@gmail.com', 'Default', 144, '0827415586'),
(142, 'test123456', 'test123456@gmail.com', 'Default', 145, '0827415586'),
(143, 'abcdef', 'abcdef@gmail.com', 'Default', 146, '0827415586'),
(144, 'abcdef', 'abcdef@gmail.com', 'Default', 147, '0827415586'),
(145, 'abcdef', 'abcdef@gmail.com', 'Default', 148, '0827415586'),
(146, 'abcdef', 'abcdef@gmail.com', 'Default', 149, '0827415586'),
(147, 'abc', 'abc@gmail.com', 'Default', 150, '0827415586'),
(148, 'abc', 'abc@gmail.com', 'Default', 151, '0827415586'),
(149, 'abc', 'abc@gmail.com', 'Default', 152, '0827415586'),
(150, 'abc', 'abc@gmail.com', 'Default', 153, '0827415586'),
(151, 'tuan123', 'tuan123@gmail.com', 'Default', 154, '0827415586'),
(152, 'tuan111', 'tuan111@gmail.com', 'Default', 155, '0827415586'),
(153, 'tuan555', 'tuan555@gmail.com', 'Default', 156, '0827415586'),
(155, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 'a', 158, '0827415586'),
(156, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 'a', 158, '0827415586'),
(157, 'Tuan Nguyen', 'nguyenhoangtuan12102003@gmail.com', 's', 158, '0827415586');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `departures`
--

CREATE TABLE `departures` (
  `departure_id` int(11) NOT NULL,
  `departure_name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `departures`
--

INSERT INTO `departures` (`departure_id`, `departure_name`) VALUES
(1, 'Hồ Chí Minh'),
(2, 'Hà Nội'),
(3, 'Đà Lạt'),
(4, 'Đà Nẵng'),
(5, 'Buôn Mê Thuật'),
(6, 'Bình Thuận'),
(7, 'Nha Trang'),
(8, 'Khánh Hòa'),
(9, 'Ninh Thuận'),
(10, 'Đồng Nai');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `detailroutes`
--

CREATE TABLE `detailroutes` (
  `detail_route_id` int(11) NOT NULL,
  `route_id` int(11) DEFAULT NULL,
  `price` double DEFAULT NULL,
  `detail_route_name` varchar(255) DEFAULT NULL,
  `description` tinytext DEFAULT NULL,
  `time_to_departure` date DEFAULT NULL,
  `time_to_finish` date DEFAULT NULL,
  `stock` int(11) DEFAULT NULL,
  `book_in_advance` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `detailroutes`
--

INSERT INTO `detailroutes` (`detail_route_id`, `route_id`, `price`, `detail_route_name`, `description`, `time_to_departure`, `time_to_finish`, `stock`, `book_in_advance`) VALUES
(1, 1, 5000000, 'Tour Trung Quốc 6N5Đ: Nghi Xương - Trương Gia Giới - Thiên Môn Sơn - Phượng Hoàng Cổ Trấn', '- Chinh phục Thiên Môn Sơn với hệ thống cáp treo dài nhất thế giới.\r\n\r\n- Khám phá vẻ đẹp huyền bí của Trương Gia Giới, nơi được ví như tiên cảnh.\r\n\r\n- Đi dạo trên Cầu Kính Đại Hiệp Cốc ở Trươn', '2024-10-28', '2024-10-31', 20, 12),
(2, 2, 1500000, 'Tour Trung Quốc 6N5Đ: Lệ Giang - Đại Lý - Shangrila (No Shopping)', '- Khám phá Lệ Giang Cổ Trấn di sản thế giới được UNESCO công nhận, nổi tiếng với kiến trúc cổ kính và hệ thống kênh rạch độc đáo.\r\n\r\n- Tham quan Đại Lý vùng đất với Tam Tháp Đại Lý nổi tiếng', '2024-10-29', '2024-11-01', 30, 16),
(3, 3, 10000000, 'Tour Thái Lan 5N4Đ: Bangkok - Pattaya - Công Viên Khủng Long (Bay Sáng, Trưa)', '- Chiêm ngưỡng tượng Phật Vàng tại chùa Wat Traimit, biểu tượng tâm linh nổi tiếng.\r\n\r\n- Khám phá Pattaya sôi động, thành phố biển không bao giờ ngủ.\r\n\r\n- Hòa mình vào thế giới tiền sử đầy ấn tượng', '2024-10-30', '2024-11-05', 25, 25),
(4, 1, 2350000, 'Tour Việt Nam 4N3Đ: Hà Nội - Hạ Long - Sapa', '- Khám phá thủ đô Hà Nội ngàn năm văn hiến.\r\n\r\n- Chiêm ngưỡng vẻ đẹp kỳ vĩ của Vịnh Hạ Long, di sản thiên nhiên thế giới.\r\n\r\n- Trải nghiệm không khí trong lành và ẩm thực địa phương tại Sapa.', '2025-01-10', '2025-01-13', 25, 25),
(5, 2, 5000000, 'Tour Việt Nam 5N4Đ: Đà Nẵng - Hội An - Mỹ Sơn', '- Tham quan thành phố Đà Nẵng năng động và các bãi biển đẹp.\r\n\r\n- Khám phá phố cổ Hội An cổ kính.\r\n\r\n- Trải nghiệm không gian linh thiêng tại thánh địa Mỹ Sơn.', '2025-01-12', '2025-01-16', 20, 10),
(6, 3, 1000000, 'Tour Việt Nam 4N3Đ: Phú Quốc - Vinpearl Safari - Chợ Đêm', '- Khám phá đảo ngọc Phú Quốc với những bãi biển hoang sơ.\r\n\r\n- Tham quan công viên Vinpearl Safari với nhiều loài động vật hoang dã.\r\n\r\n- Trải nghiệm ẩm thực tại chợ đêm Phú Quốc.', '2025-01-15', '2025-01-18', 18, 17),
(7, 1, 4500000, 'Tour Việt Nam 3N2Đ: Đà Lạt - Hồ Xuân Hương - Chùa Linh Phước', '- Khám phá thành phố ngàn hoa Đà Lạt.\r\n\r\n- Thưởng ngoạn phong cảnh tại hồ Xuân Hương.\r\n\r\n- Tham quan chùa Linh Phước nổi tiếng với kiến trúc độc đáo.', '2025-01-20', '2025-01-22', 15, 14),
(8, 2, 4000000, 'Tour Việt Nam 5N4Đ: Quy Nhơn - Ghềnh Ráng - Bãi Xép', '- Tham quan bãi biển Quy Nhơn đẹp mê hồn.\r\n\r\n- Khám phá khu du lịch Ghềnh Ráng.\r\n\r\n- Trải nghiệm biển Bãi Xép hoang sơ.', '2025-01-22', '2025-01-26', 20, 15),
(9, 3, 2500000, 'Tour Việt Nam 4N3Đ: Nha Trang - Vinpearl Land - Tháp Bà Ponagar', '- Thư giãn tại biển Nha Trang trong lành.\r\n\r\n- Trải nghiệm vui chơi tại Vinpearl Land.\r\n\r\n- Khám phá tháp Bà Ponagar với nét văn hóa Chăm Pa.', '2025-01-25', '2025-01-28', 28, 20),
(10, 4, 1300000, 'Tour Việt Nam 4N3Đ: Huế - Đại Nội - Chùa Thiên Mụ', '- Khám phá cố đô Huế với các lăng tẩm cổ kính.\r\n\r\n- Tham quan Đại Nội, di sản thế giới UNESCO.\r\n\r\n- Tận hưởng phong cảnh chùa Thiên Mụ bên sông Hương.', '2025-02-01', '2025-02-04', 22, 20),
(11, 4, 1500000, 'Tour Việt Nam 3N2Đ: Cần Thơ - Chợ Nổi Cái Răng - Nhà Cổ Bình Thủy', '- Trải nghiệm chợ nổi Cái Răng đặc trưng của miền Tây.\r\n\r\n- Khám phá nhà cổ Bình Thủy với kiến trúc Pháp.\r\n\r\n- Thưởng thức ẩm thực miền Tây dân dã.', '2025-02-05', '2025-02-07', 20, 10),
(12, 4, 1800000, 'Tour Việt Nam 5N4Đ: Hà Giang - Đồng Văn - Mèo Vạc', '- Khám phá cao nguyên đá Đồng Văn.\r\n\r\n- Chiêm ngưỡng cảnh đẹp hùng vĩ của đèo Mã Pí Lèng.\r\n\r\n- Trải nghiệm văn hóa của người dân tộc Mông tại Hà Giang.', '2025-02-10', '2025-02-14', 15, 10),
(13, 5, 2200000, 'Tour Việt Nam 4N3Đ: Mộc Châu - Đồi Chè - Thác Dải Yếm', '- Tham quan đồi chè xanh mướt tại Mộc Châu.\r\n\r\n- Khám phá thác Dải Yếm hùng vĩ.\r\n\r\n- Tận hưởng không gian trong lành của cao nguyên Mộc Châu.', '2025-02-12', '2025-02-15', 18, 10),
(14, 5, 2800000, 'Tour Việt Nam 3N2Đ: Bình Định - Eo Gió - Kỳ Co', '- Khám phá Eo Gió với bãi đá thiên nhiên độc đáo.\r\n\r\n- Tận hưởng bãi biển Kỳ Co trong xanh và hoang sơ.\r\n\r\n- Trải nghiệm làng chài yên bình.', '2025-02-20', '2025-02-22', 25, 20),
(15, 5, 3500000, 'Tour Việt Nam 5N4Đ: Phong Nha - Kẻ Bàng - Hang Sơn Đoòng', '- Khám phá vườn quốc gia Phong Nha - Kẻ Bàng.\r\n\r\n- Chiêm ngưỡng hang động đẹp nhất thế giới Sơn Đoòng.\r\n\r\n- Trải nghiệm thiên nhiên hoang sơ và hùng vĩ.', '2025-02-25', '2025-03-01', 12, 5),
(16, 6, 1600000, 'Tour Việt Nam 3N2Đ: Vũng Tàu - Bãi Trước - Tượng Chúa Ki-tô', '- Tận hưởng biển xanh và nắng vàng tại bãi Trước Vũng Tàu.\r\n\r\n- Tham quan tượng Chúa Ki-tô nổi tiếng.\r\n\r\n- Thư giãn và trải nghiệm không gian biển.', '2025-03-05', '2025-03-07', 30, 20),
(17, 6, 6200000, 'Tour Việt Nam 4N3Đ: Tây Ninh - Núi Bà Đen - Tòa Thánh Cao Đài', '- Leo núi Bà Đen nổi tiếng của Tây Ninh.\r\n\r\n- Tham quan Tòa Thánh Cao Đài với kiến trúc độc đáo.\r\n\r\n- Tận hưởng khung cảnh thiên nhiên xanh mát.', '2025-03-10', '2025-03-13', 17, 10),
(18, 6, 5500000, 'Tour Việt Nam 4N3Đ: Quảng Bình - Hang Én - Động Phong Nha', '- Khám phá hang Én với vẻ đẹp hoang sơ.\r\n\r\n- Trải nghiệm thuyền trên sông Son vào động Phong Nha.\r\n\r\n- Tận hưởng không gian yên bình của núi rừng Quảng Bình.', '2025-03-15', '2025-03-18', 14, 10),
(19, 7, 4400000, 'Tour Việt Nam 3N2Đ: Cát Bà - Vườn Quốc Gia - Đảo Khỉ', '- Khám phá thiên nhiên tuyệt đẹp tại vườn quốc gia Cát Bà.\r\n\r\n- Trải nghiệm khám phá đảo Khỉ độc đáo.\r\n\r\n- Tận hưởng không khí biển trong lành.', '2025-03-20', '2025-03-22', 16, 5),
(20, 7, 4000000, 'Tour Việt Nam 4N3Đ: Bà Rịa - Hồ Tràm - Bình Châu', '- Thư giãn tại biển Hồ Tràm và khu du lịch Bình Châu.\r\n\r\n- Tận hưởng bùn khoáng và suối nước nóng Bình Châu.\r\n\r\n- Trải nghiệm kỳ nghỉ lý tưởng tại khu du lịch.', '2025-03-25', '2025-03-28', 18, 10),
(21, 7, 2500000, 'Tour Việt Nam 5N4Đ: Miền Tây Sông Nước - An Giang - Châu Đốc', '- Khám phá chợ nổi Long Xuyên.\r\n\r\n- Trải nghiệm văn hóa người Chăm tại An Giang.\r\n\r\n- Tham quan rừng tràm Trà Sư tuyệt đẹp.', '2025-04-01', '2025-04-05', 22, 10),
(22, 8, 2000000, 'Tour Việt Nam 5N4Đ: Phong Nha - Kẻ Bàng - Hang Sơn Đoòng', '- Khám phá vườn quốc gia Phong Nha - Kẻ Bàng.\r\n\r\n- Chiêm ngưỡng hang động đẹp nhất thế giới Sơn Đoòng.\r\n\r\n- Trải nghiệm thiên nhiên hoang sơ và hùng vĩ.', '2025-02-25', '2025-03-01', 12, 10),
(23, 9, 2800000, 'Tour Việt Nam 3N2Đ: Vũng Tàu - Bãi Trước - Tượng Chúa Ki-tô', '- Tận hưởng biển xanh và nắng vàng tại bãi Trước Vũng Tàu.\r\n\r\n- Tham quan tượng Chúa Ki-tô nổi tiếng.\r\n\r\n- Thư giãn và trải nghiệm không gian biển.', '2025-03-05', '2025-03-07', 30, 25),
(24, 9, 3200000, 'Tour Việt Nam 4N3Đ: Tây Ninh - Núi Bà Đen - Tòa Thánh Cao Đài', '- Leo núi Bà Đen nổi tiếng của Tây Ninh.\r\n\r\n- Tham quan Tòa Thánh Cao Đài với kiến trúc độc đáo.\r\n\r\n- Tận hưởng khung cảnh thiên nhiên xanh mát.', '2025-03-10', '2025-03-13', 17, 10),
(25, 10, 3500000, 'Tour Việt Nam 4N3Đ: Quảng Bình - Hang Én - Động Phong Nha', '- Khám phá hang Én với vẻ đẹp hoang sơ.\r\n\r\n- Trải nghiệm thuyền trên sông Son vào động Phong Nha.\r\n\r\n- Tận hưởng không gian yên bình của núi rừng Quảng Bình.', '2025-03-15', '2025-03-18', 14, 4),
(26, 10, 3000000, 'Tour Việt Nam 3N2Đ: Cát Bà - Vườn Quốc Gia - Đảo Khỉ', '- Khám phá thiên nhiên tuyệt đẹp tại vườn quốc gia Cát Bà.\r\n\r\n- Trải nghiệm khám phá đảo Khỉ độc đáo.\r\n\r\n- Tận hưởng không khí biển trong lành.', '2025-03-20', '2025-03-22', 16, 10),
(27, 10, 2800000, 'Tour Việt Nam 4N3Đ: Bà Rịa - Hồ Tràm - Bình Châu', '- Thư giãn tại biển Hồ Tràm và khu du lịch Bình Châu.\r\n\r\n- Tận hưởng bùn khoáng và suối nước nóng Bình Châu.\r\n\r\n- Trải nghiệm kỳ nghỉ lý tưởng tại khu du lịch.', '2025-03-25', '2025-03-28', 18, 10),
(28, 10, 4000000, 'Tour Việt Nam 5N4Đ: Miền Tây Sông Nước - An Giang - Châu Đốc', '- Khám phá chợ nổi Long Xuyên.\r\n\r\n- Trải nghiệm văn hóa người Chăm tại An Giang.\r\n\r\n- Tham quan rừng tràm Trà Sư tuyệt đẹp.', '2025-04-01', '2025-04-05', 22, 15),
(29, 8, 4500000, 'Tour Việt Nam 3N2Đ: Bình Định - Eo Gió - Kỳ Co', '- Khám phá Eo Gió với bãi đá thiên nhiên độc đáo.\r\n\r\n- Tận hưởng bãi biển Kỳ Co trong xanh và hoang sơ.\r\n\r\n- Trải nghiệm làng chài yên bình.', '2025-02-20', '2025-02-22', 25, 20),
(30, 6, 2400000, 'Tour du lịch Bình Thuận Hà Nội', 'aaa', '2024-11-07', '2024-11-30', 30, 20),
(31, 7, 10000000, 'Tour du lịch Nha Trang - Binh Thuan', 'chuyen di', '2024-11-06', '2024-11-09', 30, 20);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `employees`
--

CREATE TABLE `employees` (
  `employee_id` varchar(255) NOT NULL,
  `employee_email` varchar(255) NOT NULL,
  `user_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `employees`
--

INSERT INTO `employees` (`employee_id`, `employee_email`, `user_id`) VALUES
('NV_01', 'NV_10@gmail.com', 119),
('NV_02', 'NV_01@gmail.com', 120),
('NV_03', 'NV_02@gmail.com', 121),
('NV_04', 'NV_03@gmail.com', 122);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `feedback`
--

CREATE TABLE `feedback` (
  `feedback_id` int(11) NOT NULL,
  `booking_id` int(11) DEFAULT NULL,
  `detail_route_id` int(11) DEFAULT NULL,
  `text` varchar(255) NOT NULL,
  `rating` float NOT NULL,
  `date_create` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `feedback`
--

INSERT INTO `feedback` (`feedback_id`, `booking_id`, `detail_route_id`, `text`, `rating`, `date_create`) VALUES
(4, 17, 3, 'Cô trưởng đoàn Hồ thị mỹ Ly rất dễ thương và tận tình với khách hàng. Tôi tặng 5 sao cho sự chuyên nghiệp và sự quan tâm đến khách hàng cho cô Mỹ Ly. Khách sạn nên nâng cấp lên chút nữa thì tuyệt vời. Brandon.', 5, '2024-11-13 00:22:34.000000'),
(5, 18, 3, 'Rất thân thiện. Chỉ có ăn sáng món ăn lập lại nên hơi không ngon miệng.', 4.5, '2024-11-30 00:09:36.000000'),
(6, 17, 29, 'Tốt dịch vụ vui vẽ hài lòng\r\n', 4.8, '2024-11-27 00:09:53.000000'),
(7, 19, 2, 'Tôi cảm thấy hài lòng với tour du lịch , gồm những gói dịch vụ chu đáo, tận tình', 5, '2024-11-29 00:10:18.000000'),
(8, 19, 16, 'Tôi cảm thấy hài lòng với dịch vụ', 4, '2024-11-21 10:11:56.000000'),
(9, 20, 3, 'Chưa hài lòng với món ăn và dịch vụ của tour ', 3.5, '2024-11-22 10:12:19.000000'),
(11, 18, 3, 'Tuyệt vời ', 5, '2024-11-20 10:13:18.000000'),
(12, 19, 1, 'Ở tour du lịch giúp tôi trải nghiệm thêm nhiều điều mới mẻ , rất hài lòng !', 5, '2024-11-26 10:13:32.000000'),
(13, 17, 6, 'Tuyệt vời , tôi sẽ quay lại !\r\n', 5, '0000-00-00 00:00:00.000000');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `images`
--

CREATE TABLE `images` (
  `image_id` int(11) NOT NULL,
  `text_image` tinytext DEFAULT NULL,
  `detail_route_id` int(11) NOT NULL,
  `is_primary` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `images`
--

INSERT INTO `images` (`image_id`, `text_image`, `detail_route_id`, `is_primary`) VALUES
(1, 'item_1.jpg', 1, 1),
(2, 'item_2.jpg', 1, 0),
(3, 'item_3.jpg', 1, 0),
(4, 'item_4.jpg', 1, 0),
(5, 'item_5.jpg', 2, 1),
(6, 'item_6.jpg', 2, 0),
(7, 'item_7.jpg', 2, 0),
(8, 'item_8.jpg', 2, 0),
(9, 'item_9.jpg', 3, 1),
(10, 'item_10.jpg', 3, 0),
(11, 'item_11.jpg', 3, 0),
(12, 'item_12.jpg', 3, 0),
(13, 'item_13.jpg', 4, 1),
(14, 'item_14.jpg', 4, 0),
(15, 'item_15.jpg', 4, 0),
(16, 'item_16.jpg', 4, 0),
(17, 'item_17.jpg', 5, 1),
(18, 'item_18.jpg', 5, 0),
(19, 'item_19.jpg', 5, 0),
(20, 'item_20.jpg', 5, 0),
(21, 'item_21.jpg', 6, 1),
(22, 'item_22.jpg', 6, 0),
(23, 'item_23.jpg', 6, 0),
(24, 'item_24.jpg', 6, 0),
(25, 'item_25.jpg', 7, 1),
(26, 'item_26.jpg', 7, 0),
(27, 'item_27.jpg', 7, 0),
(28, 'item_28.jpg', 7, 0),
(29, 'item_29.jpg', 8, 1),
(30, 'item_30.jpg', 8, 0),
(31, 'item_31.jpg', 8, 0),
(32, 'item_32.jpg', 8, 0),
(33, 'item_33.jpg', 9, 1),
(34, 'item_34.jpg', 9, 0),
(35, 'item_35.jpg', 9, 0),
(36, 'item_36.jpg', 9, 0),
(37, 'item_37.jpg', 10, 1),
(38, 'item_38.jpg', 10, 0),
(39, 'item_39.jpg', 10, 0),
(40, 'item_40.jpg', 10, 0),
(41, 'item_41.jpg', 11, 1),
(42, 'item_42.jpg', 11, 0),
(43, 'item_43.jpg', 11, 0),
(44, 'item_44.jpg', 11, 0),
(45, 'item_45.jpg', 12, 1),
(46, 'item_46.jpg', 12, 0),
(47, 'item_47.jpg', 12, 0),
(48, 'item_48.jpg', 12, 0),
(49, 'item_49.jpg', 13, 1),
(50, 'item_50.jpg', 13, 0),
(51, 'item_51.jpg', 13, 0),
(52, 'item_52.jpg', 13, 0),
(53, 'item_53.jpg', 14, 1),
(54, 'item_54.jpg', 14, 0),
(55, 'item_55.jpg', 14, 0),
(56, 'item_56.jpg', 14, 0),
(57, 'item_57.jpg', 15, 1),
(58, 'item_58.jpg', 15, 0),
(59, 'item_59.jpg', 15, 0),
(60, 'item_60.jpg', 15, 0),
(61, 'item_61.jpg', 16, 1),
(62, 'item_62.jpg', 16, 0),
(63, 'item_63.jpg', 16, 0),
(64, 'item_64.jpg', 16, 0),
(65, 'item_65.jpg', 17, 1),
(66, 'item_66.jpg', 17, 0),
(67, 'item_67.jpg', 17, 0),
(68, 'item_68.jpg', 17, 0),
(69, 'item_69.jpg', 18, 1),
(70, 'item_70.jpg', 18, 0),
(71, 'item_71.jpg', 18, 0),
(72, 'item_72.jpg', 18, 0),
(73, 'item_73.jpg', 19, 1),
(74, 'item_74.jpg', 19, 0),
(75, 'item_75.jpg', 19, 0),
(76, 'item_76.jpg', 19, 0),
(77, 'item_1.jpg', 20, 1),
(78, 'item_2.jpg', 20, 0),
(79, 'item_3.jpg', 20, 0),
(80, 'item_4.jpg', 20, 0),
(81, 'item_5.jpg', 21, 1),
(82, 'item_6.jpg', 21, 0),
(83, 'item_7.jpg', 21, 0),
(84, 'item_8.jpg', 21, 0),
(85, 'item_9.jpg', 22, 1),
(86, 'item_10.jpg', 22, 0),
(87, 'item_11.jpg', 22, 0),
(88, 'item_12.jpg', 22, 0),
(89, 'item_13.jpg', 23, 1),
(90, 'item_14.jpg', 23, 0),
(91, 'item_15.jpg', 23, 0),
(92, 'item_16.jpg', 23, 0),
(93, 'item_17.jpg', 24, 1),
(94, 'item_18.jpg', 24, 0),
(95, 'item_19.jpg', 24, 0),
(96, 'item_20.jpg', 24, 0),
(97, 'item_21.jpg', 25, 1),
(98, 'item_22.jpg', 25, 0),
(99, 'item_23.jpg', 25, 0),
(100, 'item_24.jpg', 25, 0),
(101, 'item_25.jpg', 26, 1),
(102, 'item_26.jpg', 26, 0),
(103, 'item_27.jpg', 26, 0),
(104, 'item_28.jpg', 26, 0),
(105, 'item_29.jpg', 27, 1),
(106, 'item_30.jpg', 27, 0),
(107, 'item_31.jpg', 27, 0),
(108, 'item_32.jpg', 27, 0),
(109, 'item_33.jpg', 28, 1),
(110, 'item_34.jpg', 28, 0),
(111, 'item_35.jpg', 28, 0),
(112, 'item_36.jpg', 28, 0),
(113, 'item_37.jpg', 29, 1),
(114, 'item_38.jpg', 29, 0),
(115, 'item_39.jpg', 29, 0),
(116, 'item_40.jpg', 29, 0),
(117, 'item_14.jpg', 30, 1),
(118, 'item_10.jpg', 30, 0),
(119, 'item_7.jpg', 30, 0),
(120, 'item_6.jpg', 30, 0),
(121, 'item_4.jpg', 31, 1),
(122, 'item_10.jpg', 31, 0);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `invalidated_token`
--

CREATE TABLE `invalidated_token` (
  `id` varchar(255) NOT NULL,
  `expiry_time` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `invalidated_token`
--

INSERT INTO `invalidated_token` (`id`, `expiry_time`) VALUES
('0104d113-61a5-4dc2-b17c-5e191b675bb5', '2024-11-20 12:01:13.000000'),
('02bda4b9-30df-4fdd-8616-ce9e1aeb0087', '2024-11-20 21:02:13.000000'),
('04eaf8f6-75df-4fe0-8560-7d72fb3c0dea', '2024-11-19 14:18:13.000000'),
('05f5af47-e222-4c21-81fa-9ef8146d4ad2', '2024-11-20 11:41:41.000000'),
('0a903044-d969-45c4-895c-3a66e124293e', '2024-11-21 14:33:44.000000'),
('0aa77234-f460-4be8-a5a9-ec8a67c4d6a4', '2024-11-18 23:05:49.000000'),
('0f6fa74a-a3bf-46d8-aec1-84146c28ecfc', '2024-11-19 14:17:13.000000'),
('15389c2e-3330-476d-96d7-dcff7f1532a6', '2024-11-20 23:14:13.000000'),
('15d69e43-06d4-4a29-bcee-bd9e4ced175e', '2024-11-21 13:24:40.000000'),
('1711d873-0188-4463-ab97-b1a4ad541859', '2024-11-21 14:32:44.000000'),
('1977abf3-6ae4-4c39-bd31-a7570cd22e78', '2024-11-21 12:56:11.000000'),
('1c067ed8-8c5d-4f35-9aaf-f90ed128edc8', '2024-11-20 23:18:13.000000'),
('1ce5b67a-c6cc-4c85-8025-b3456bc00202', '2024-11-21 12:58:15.000000'),
('1e65bcfd-71d0-49f4-82c8-ab3dc81a7189', '2024-11-21 14:36:43.000000'),
('1fc009d3-30c7-49d6-9e75-09020b362370', '2024-11-20 21:15:07.000000'),
('20f74082-3578-4c77-a320-b977acf79cd6', '2024-11-21 13:23:39.000000'),
('2112f964-f2f8-460a-9c20-46834fe66f65', '2024-11-20 21:18:13.000000'),
('2679fffa-710a-4512-8e88-c9c887a69af8', '2024-11-20 21:12:07.000000'),
('290a3e57-2770-4c19-b99b-342a4f240116', '2024-11-20 20:49:04.000000'),
('2c8ba535-b0bf-4ddd-8231-b91067e6da0f', '2024-11-20 21:24:13.000000'),
('2e759a14-9a65-44d4-84c3-cfb8df866347', '2024-11-20 21:07:54.000000'),
('2e9c4bc8-dedc-426a-b921-ecfd8d53d2fb', '2024-11-20 21:28:42.000000'),
('2f634d60-7e73-48b5-b106-b2bce6f1a633', '2024-11-20 22:51:45.000000'),
('2fa352dc-df50-4685-a52e-a8a678c89ef1', '2024-11-21 13:31:39.000000'),
('3092567a-e124-4314-b5c1-59523e28e536', '2024-11-20 23:03:49.000000'),
('3121c493-28f7-4c00-85c8-bdc38fd8e42b', '2024-11-21 13:02:33.000000'),
('33a27247-e850-46a7-8fcd-bf08bf594dfd', '2024-11-20 21:22:13.000000'),
('364fe99f-324c-4627-92e6-f0035c89b0aa', '2024-11-20 23:17:13.000000'),
('36d8ba64-3b90-4461-9a09-a04e59beb52f', '2024-11-21 14:19:11.000000'),
('3ae0aa91-ec94-4174-b32a-343b2b02ae00', '2024-11-20 23:15:13.000000'),
('3afcf769-ff6b-42d8-8c2f-ffb06a48a23a', '2024-11-21 13:01:33.000000'),
('3d7a6d55-688f-4cfc-8016-a3eb7a7145e3', '2024-11-21 13:03:48.000000'),
('3ecdadaf-4936-4e1a-931f-7f317da34ce1', '2024-11-21 12:41:04.000000'),
('40ba89c8-a9fc-436e-bc8b-1ecd6003352d', '2024-11-20 21:25:13.000000'),
('40db1041-65da-492f-908d-a52e0a18408c', '2024-11-21 13:11:55.000000'),
('4273423a-3c23-4a1b-867c-1392a3837c5d', '2024-11-20 21:04:13.000000'),
('473ca1c3-08d1-4286-bd6c-fbbeb10b26f6', '2024-11-20 23:11:49.000000'),
('48517677-5c65-429f-9784-3c13cc4ceb2b', '2024-11-20 21:00:12.000000'),
('4b3ca1b6-38af-4ee6-b52d-3f3a4b5b4013', '2024-11-21 12:39:03.000000'),
('4b668d76-5d8e-4f2c-b2c5-ba32921924e9', '2024-11-21 14:18:10.000000'),
('4d2d2485-3a0a-4d3c-b3e6-a6ff62339f67', '2024-11-21 14:36:10.000000'),
('4dec7131-3380-4f24-9701-2daf142ada89', '2024-11-21 12:55:10.000000'),
('533fa0ca-ebdd-490f-b44f-5258b3c708d4', '2024-11-21 13:34:39.000000'),
('54819331-18d7-438e-ba8b-d85b651a7048', '2024-11-20 21:21:13.000000'),
('54a44de9-27b9-4ab2-a754-38cea33eba82', '2024-11-20 22:59:14.000000'),
('55fc6ebf-2217-43ef-8595-530c55a9e209', '2024-11-21 14:30:43.000000'),
('57a57e8d-8b25-43bb-8322-47c111f4bde9', '2024-11-21 13:35:39.000000'),
('583f8011-2db7-4476-968d-270f2675c8dd', '2024-11-21 13:27:45.000000'),
('5a5a3f12-f999-42fe-afe5-326a3bd54c5f', '2024-11-21 12:47:10.000000'),
('5b631858-d3e4-4ae2-b1c2-e3c360194fa7', '2024-11-21 13:07:14.000000'),
('5f15f339-32de-4f98-a9c4-cec2ab2bda74', '2024-11-21 12:45:10.000000'),
('61f8b313-e570-45fa-a5d5-807ea2b9eb59', '2024-11-21 12:46:10.000000'),
('67e3c461-29ad-45b3-be8e-513b6aee7901', '2024-11-20 23:11:01.000000'),
('6959b23c-e8e8-4d42-89e3-0b7abbe19b9a', '2024-11-20 21:19:13.000000'),
('695a533f-0df3-485c-9c0b-42e37984104a', '2024-11-20 21:03:13.000000'),
('69b3469f-c64b-4511-990d-c6bfae70991b', '2024-11-20 21:26:13.000000'),
('69d32572-bbfd-4248-876e-aeebda87e0b0', '2024-11-21 12:42:04.000000'),
('6bd50c65-851d-453b-855b-40eb42d17e9f', '2024-11-20 21:01:13.000000'),
('6e4c90ee-c608-4472-8013-7f6ad628a59f', '2024-11-20 22:49:45.000000'),
('6ee6491c-111b-4130-a0d4-cd3bf67d43ae', '2024-11-20 22:58:14.000000'),
('717ef6e7-e6ff-4c5c-ac2b-d67b60bf1f0e', '2024-11-20 23:07:49.000000'),
('78b75239-6190-4720-9124-c7a14161979e', '2024-11-20 23:12:49.000000'),
('79a7656a-77a9-417f-a1db-3d9413456ca8', '2024-11-18 23:41:06.000000'),
('7bbb21f9-1c87-4e30-b691-c458048de50e', '2024-11-19 14:20:14.000000'),
('7d233282-6103-4930-815a-746d0919e9e5', '2024-11-19 14:16:13.000000'),
('836b4b9d-8056-4f8d-a21d-b6ed96d46cd9', '2024-11-19 13:44:37.000000'),
('858d51ed-8c63-4352-8c0f-37c5c0774c77', '2024-11-20 21:16:13.000000'),
('86680804-c373-438c-acf6-81b12c3b0854', '2024-11-20 11:39:40.000000'),
('86af2860-9b1c-4ac6-ad0c-fe36a885edc3', '2024-11-20 21:06:53.000000'),
('8af6f8e6-97e1-424c-9d43-f1b7e72f2e5c', '2024-11-21 14:34:44.000000'),
('8ce09bb3-1518-4105-93c3-17add62f13f7', '2024-11-20 20:50:05.000000'),
('8d882af1-edec-4271-ac0d-56fea331a576', '2024-11-20 23:19:13.000000'),
('91955f57-118f-42ac-956f-c4a3a36b280d', '2024-11-20 11:40:40.000000'),
('93639800-b88e-4304-a6ba-6840335fc98a', '2024-11-21 13:25:40.000000'),
('93675902-59ad-4e78-9df9-731ebcd8553e', '2024-11-20 23:04:49.000000'),
('93f7ccb4-f4d9-41b2-849c-cd32231a9739', '2024-11-19 13:49:41.000000'),
('95c5d740-14b2-4049-b513-70ea4b3306fa', '2024-11-20 21:13:07.000000'),
('9b331024-806e-4b73-b0a7-24bf31c1fc1c', '2024-11-19 14:22:50.000000'),
('a1faa086-39a3-4d1c-aa1b-9e38e74aaad1', '2024-11-20 23:10:13.000000'),
('a2997934-2f40-43e5-9ca2-77852242f964', '2024-11-21 12:40:04.000000'),
('a33f6777-2557-49b9-95e5-6d2d71f002e1', '2024-11-21 14:17:10.000000'),
('a37ec503-37c7-4823-95b9-dd05e9f5c4d9', '2024-11-19 14:23:50.000000'),
('ad95a77d-031d-49f2-95a9-0938f7afa87d', '2024-11-21 12:43:04.000000'),
('ae676ccd-858c-44b5-a0c0-a424725f2515', '2024-11-21 12:59:15.000000'),
('afe77f59-68b0-4fee-bbb8-aaf100729075', '2024-11-20 20:53:04.000000'),
('b27bf147-4edc-430a-8e1c-6041e84e728d', '2024-11-21 14:16:10.000000'),
('b49c9006-cb89-4c03-a59b-481eb627a6fd', '2024-11-20 23:16:13.000000'),
('b5d2041d-f903-4c7d-a70a-94e60b9762b0', '2024-11-20 21:27:13.000000'),
('bbd11ccf-b732-49fe-a90d-c12d758c173d', '2024-11-21 13:26:40.000000'),
('bd6b8372-c32e-47f0-98f5-99968f1697e1', '2024-11-20 22:54:40.000000'),
('bdfe051f-dc31-4584-912e-75195a4dcff1', '2024-11-20 22:52:45.000000'),
('c34053ef-a9d0-4f6b-b191-99e5a254f6f0', '2024-11-20 21:17:13.000000'),
('c44a6dc0-ed6b-471a-840d-41a8908bd7ef', '2024-11-19 14:02:57.000000'),
('c458eee5-2167-42a8-bf2f-8da98f18fa38', '2024-11-21 15:42:22.000000'),
('c59ed00e-5010-4994-972c-80685a28e87a', '2024-11-20 15:36:58.000000'),
('c5a25985-d49c-4b9e-b87d-13b4752ba012', '2024-11-21 12:48:10.000000'),
('c61be4f4-9b97-48a0-b43b-63318f0b00ad', '2024-11-20 20:52:05.000000'),
('c68356f9-ca79-4fbe-a4bf-82e5d9c15164', '2024-11-21 13:33:39.000000'),
('c96377e7-1fa5-4f3c-94b7-1a65daa1468d', '2024-11-20 21:05:13.000000'),
('d1d636bd-3361-4cb5-8837-ee2ad8c26f5f', '2024-11-20 23:09:13.000000'),
('d67fc2d8-6d8b-4ad2-98d8-83773bc60cb2', '2024-11-21 12:44:10.000000'),
('d6d03348-d4a4-4112-8445-ad003a56d44a', '2024-11-20 23:06:49.000000'),
('d6e61a77-5cdf-4d2a-aefb-94aa89ac12d4', '2024-11-21 14:31:44.000000'),
('d744aa47-73c0-48e7-a7c0-a58150af99df', '2024-11-21 15:41:21.000000'),
('d8d08b89-00bb-4fba-988b-5cf53b831bde', '2024-11-19 14:19:13.000000'),
('da06b1ca-ee7e-42cb-90c8-4caa9dc5bf29', '2024-11-20 21:14:07.000000'),
('dc1fbef3-8cb4-4c43-ba52-b2f41b6d0def', '2024-11-20 23:05:49.000000'),
('de9b255d-9f63-4ad0-9ff1-809ba8aa6472', '2024-11-21 14:20:11.000000'),
('df43217f-4a20-4317-9b1f-9de4c976873a', '2024-11-21 13:32:39.000000'),
('e6d8e209-5d2c-4c7d-995a-a54c0866aa5e', '2024-11-20 15:37:59.000000'),
('e909babe-d3de-4df2-a172-10d38c025745', '2024-11-20 21:11:06.000000'),
('eba30aeb-0ecd-451c-a7cb-91ead1d58a5b', '2024-11-21 13:22:10.000000'),
('ec8a5059-426b-4387-be7e-d64acde3251a', '2024-11-21 13:18:23.000000'),
('f0deff51-0311-4a6f-ab97-2337eba1e7f2', '2024-11-19 14:21:14.000000'),
('f1315eb0-ec85-4457-863e-f7c2eceb6f71', '2024-11-20 20:51:05.000000'),
('f1b4139e-cd15-416c-b1ff-ec35524fd6b2', '2024-11-20 21:20:13.000000'),
('f37c7134-26a1-4e89-878d-509a9e34a710', '2024-11-21 12:57:11.000000'),
('f3b2ca75-5a86-4543-bb08-ea88be391e84', '2024-11-21 13:19:53.000000'),
('f4e1422c-a466-476f-be0f-46b0801bab36', '2024-11-20 21:23:13.000000'),
('f515776a-eaea-49ba-8046-ca2fc2100157', '2024-11-20 22:50:45.000000'),
('f548c6e1-b33a-4272-abd8-3290a4758b7d', '2024-11-19 14:14:11.000000'),
('f8556c0d-ed0f-4b44-8b6b-3ffd80eae1bc', '2024-11-21 13:09:49.000000');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `legs`
--

CREATE TABLE `legs` (
  `leg_id` int(11) NOT NULL,
  `detail_route_id` int(11) DEFAULT NULL,
  `title` varchar(255) DEFAULT NULL,
  `description` tinytext DEFAULT NULL,
  `sequence` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `legs`
--

INSERT INTO `legs` (`leg_id`, `detail_route_id`, `title`, `description`, `sequence`) VALUES
(1, 1, 'TP.HCM - Lệ Giang ( Ăn tối )', '11h00: Quý khách có mặt tại sân bay Tân Sơn Nhất ga đi quốc tế. Trưởng Đoàn hướng dẫn làm thủ tục chuyến DR5052 lúc 14:05 - 18:35 đi Lệ Giang.\r\n\r\nĐoàn đến sân bay Lệ Giang, HDV đón đoàn ăn tối và về ', 1),
(2, 1, 'Lệ Giang - Đại lý (Ăn Sáng, trưa , tối)', 'Đoàn dùng bữa sáng tại khách sạn, khởi hành tham quan:\r\n\r\nThi Trấn ShuangLang - Làng chài có lịch sử hơn 1000 năm nằm ở phía Bắc thành phố Đại Lý, thị trấn ven hồ Nhĩ Hải rất được du khách yêu thích', 2),
(3, 1, 'Đại Lý - SHANGRILA ( Ăn sáng, trưa , tối )', 'Đoàn dùng bữa sáng tại khách sạn, khởi hành tham quan:\r\n\r\nThành cổ Đại Lý - Huyền thoại giữa núi Thương Sơn và hồ Nhĩ Hải, nơi Tam tháp và di sản Phật giáo, cũng là quê hương thái tử Đoàn Dự từ \'T', 3),
(4, 1, 'SHANGRILA - Lệ Giang ( Ăn sáng , trưa , tối )', 'Đoàn dùng bữa sáng tại khách sạn, tham quan:\r\n\r\nTu viện Songzanlin - Tu viện Phật giáo Tây Tạng lớn nhất ở tỉnh Vân Nam, cũng là một trong những tu viện nổi tiếng ở huyện Kham và Giáo phái Vàng ở Tứ Xu', 4),
(5, 2, 'Hà Nội - Sapa (Ăn tối)', '11h00: Quý khách có mặt tại sân bay Nội Bài. Trưởng Đoàn hướng dẫn làm thủ tục chuyến bay đi Sapa. Đoàn đến Sapa, HDV đón đoàn ăn tối và nghỉ ngơi.', 1),
(6, 2, 'Sapa - Fansipan (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, khởi hành tham quan:\r\n\r\nChinh phục đỉnh Fansipan - nóc nhà Đông Dương, với cảnh đẹp tuyệt vời.', 2),
(7, 2, 'Sapa - Hà Nội (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, tham quan:\r\n\r\nLàng dân tộc người Mông, tìm hiểu văn hóa và phong tục tập quán.', 3),
(8, 2, 'Hà Nội - Ninh Bình (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, khởi hành đi Ninh Bình, tham quan Tam Cốc - Bích Động.', 4),
(9, 3, 'Đà Nẵng - Hội An (Ăn tối)', 'Quý khách có mặt tại sân bay Đà Nẵng. Trưởng Đoàn hướng dẫn di chuyển về Hội An, ăn tối và nghỉ ngơi.', 1),
(10, 3, 'Hội An - Mỹ Sơn (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, tham quan Mỹ Sơn - di sản văn hóa thế giới.', 2),
(11, 3, 'Hội An - Đà Nẵng (Ăn sáng, trưa, tối)', 'Đoàn tham quan phố cổ Hội An, tìm hiểu về kiến trúc và văn hóa nơi đây.', 3),
(12, 3, 'Đà Nẵng - Bà Nà Hills (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, khởi hành tham quan Bà Nà Hills - chốn bồng lai tiên cảnh.', 4),
(13, 4, 'Nha Trang - Đà Lạt (Ăn tối)', 'Quý khách có mặt tại sân bay Cam Ranh, di chuyển về Đà Lạt, ăn tối và nghỉ ngơi.', 1),
(14, 4, 'Đà Lạt - Thung Lũng Tình Yêu (Ăn sáng, trưa, tối)', 'Đoàn tham quan Thung Lũng Tình Yêu, chiêm ngưỡng phong cảnh hữu tình.', 2),
(15, 4, 'Đà Lạt - Nha Trang (Ăn sáng, trưa, tối)', 'Quý khách tham quan chợ Đà Lạt, tìm hiểu văn hóa địa phương.', 3),
(16, 4, 'Nha Trang - Vinpearl Land (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng, tham gia các trò chơi thú vị tại Vinpearl Land.', 4),
(17, 5, 'Hà Nội - Quảng Ninh (Ăn tối)', 'Quý khách có mặt tại sân bay Nội Bài, di chuyển đến Quảng Ninh, ăn tối và nghỉ ngơi.', 1),
(18, 5, 'Quảng Ninh - Hạ Long (Ăn sáng, trưa, tối)', 'Đoàn tham quan Vịnh Hạ Long - di sản thiên nhiên thế giới.', 2),
(19, 5, 'Hạ Long - Hà Nội (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng trên tàu, tham quan các hang động đẹp.', 3),
(20, 5, 'Hà Nội - Mộc Châu (Ăn sáng, trưa, tối)', 'Đoàn di chuyển đến Mộc Châu, thưởng thức các món ăn đặc sản.', 4),
(21, 6, 'TP.HCM - Vũng Tàu (Ăn tối)', 'Quý khách có mặt tại TP.HCM, di chuyển đến Vũng Tàu, ăn tối và nghỉ ngơi.', 1),
(22, 6, 'Vũng Tàu - Bãi Sau (Ăn sáng, trưa, tối)', 'Đoàn tham quan Bãi Sau, tắm biển và thư giãn.', 2),
(23, 6, 'Bãi Sau - TP.HCM (Ăn sáng, trưa, tối)', 'Quý khách dùng bữa sáng tại khách sạn, di chuyển về TP.HCM.', 3),
(24, 6, 'TP.HCM - Côn Đảo (Ăn sáng, trưa, tối)', 'Đoàn bay đi Côn Đảo, tham quan các địa điểm lịch sử.', 4),
(25, 7, 'TP.HCM - Phú Quốc (Ăn tối)', 'Quý khách có mặt tại sân bay Tân Sơn Nhất, bay đi Phú Quốc, ăn tối và nghỉ ngơi.', 1),
(26, 7, 'Phú Quốc - Bãi Sao (Ăn sáng, trưa, tối)', 'Đoàn tham quan Bãi Sao, một trong những bãi biển đẹp nhất Phú Quốc.', 2),
(27, 7, 'Bãi Sao - TP.HCM (Ăn sáng, trưa, tối)', 'Quý khách dùng bữa sáng, di chuyển về TP.HCM.', 3),
(28, 7, 'TP.HCM - Hạ Long (Ăn sáng, trưa, tối)', 'Đoàn khởi hành đi Hạ Long, thưởng thức các món ăn địa phương.', 4),
(29, 8, 'TP.HCM - Đà Nẵng (Ăn tối)', 'Quý khách có mặt tại sân bay Tân Sơn Nhất, bay đi Đà Nẵng, ăn tối và nghỉ ngơi.', 1),
(30, 8, 'Đà Nẵng - Ngũ Hành Sơn (Ăn sáng, trưa, tối)', 'Đoàn tham quan Ngũ Hành Sơn, khám phá các hang động.', 2),
(31, 8, 'Ngũ Hành Sơn - Hội An (Ăn sáng, trưa, tối)', 'Quý khách tham quan phố cổ Hội An, tìm hiểu văn hóa.', 3),
(32, 8, 'Hội An - Đà Nẵng (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, di chuyển về Đà Nẵng.', 4),
(33, 9, 'Hà Nội - Ninh Bình (Ăn tối)', 'Quý khách có mặt tại sân bay Nội Bài, di chuyển đến Ninh Bình, ăn tối và nghỉ ngơi.', 1),
(34, 9, 'Ninh Bình - Tràng An (Ăn sáng, trưa, tối)', 'Đoàn tham quan Tràng An, một trong những di sản thiên nhiên thế giới.', 2),
(35, 9, 'Tràng An - Hà Nội (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại nhà hàng, tham quan các địa điểm nổi tiếng tại Ninh Bình trước khi trở về Hà Nội.', 3),
(36, 9, 'Hà Nội - Hải Phòng (Ăn sáng, trưa, tối)', 'Quý khách khởi hành đi Hải Phòng, tham quan Đồ Sơn và thưởng thức hải sản.', 4),
(37, 10, 'TP.HCM - Cần Thơ (Ăn tối)', 'Quý khách có mặt tại sân bay Tân Sơn Nhất, bay đi Cần Thơ, ăn tối và nghỉ ngơi.', 1),
(38, 10, 'Cần Thơ - Chợ Nổi Cái Răng (Ăn sáng, trưa, tối)', 'Đoàn tham quan Chợ Nổi Cái Răng, tìm hiểu đời sống người dân miền Tây.', 2),
(39, 10, 'Cần Thơ - TP.HCM (Ăn sáng, trưa, tối)', 'Đoàn dùng bữa sáng tại khách sạn, di chuyển về TP.HCM, kết thúc chương trình.', 3),
(40, 10, 'TP.HCM - Vĩnh Long (Ăn sáng, trưa, tối)', 'Quý khách khởi hành đến Vĩnh Long, tham quan các vườn trái cây đặc sản.', 4),
(41, 30, 'Ngày 1', 'A', 1),
(42, 30, 'Ngày 2', 'b', 2),
(43, 30, 'Ngày 3', 'c', 3),
(44, 30, 'Ngày 4', 'd', 4),
(45, 31, 'Ngày 1', 'đi chơi', 1),
(46, 31, 'Ngày 2', 'đi dạo', 2);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `objects`
--

CREATE TABLE `objects` (
  `object_id` int(11) NOT NULL,
  `object_name` varchar(255) DEFAULT NULL,
  `description` tinytext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `objects`
--

INSERT INTO `objects` (`object_id`, `object_name`, `description`) VALUES
(1, 'Người lớn', 'Độ tuổi > 20'),
(2, 'Trẻ em', 'Độ tuổi từ 5-12'),
(3, 'Em bé', 'Từ 0->5');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `operations`
--

CREATE TABLE `operations` (
  `operation_id` int(11) NOT NULL,
  `operation_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `operations`
--

INSERT INTO `operations` (`operation_id`, `operation_name`) VALUES
(1, 'Add'),
(2, 'Edit'),
(3, 'Delete');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `passengers`
--

CREATE TABLE `passengers` (
  `passenger_id` int(11) NOT NULL,
  `object_id` int(11) DEFAULT NULL,
  `passenger_name` varchar(255) NOT NULL,
  `gender` varchar(255) NOT NULL,
  `date_birth` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `passengers`
--

INSERT INTO `passengers` (`passenger_id`, `object_id`, `passenger_name`, `gender`, `date_birth`) VALUES
(1, 1, 'abc', 'nam', '2024-11-12'),
(4, 1, 'asdf', 'Nam', '2024-11-28'),
(5, 1, 'asdf', 'Nam', '2024-11-28'),
(6, 2, 'asdfs', 'Nam', '2024-11-12'),
(7, 1, '', 'Nam', '2024-11-26'),
(8, 1, '', 'Nam', '2024-10-29'),
(9, 1, '', 'Nam', '2024-11-19'),
(10, 2, '', 'Nam', '2024-11-06'),
(11, 1, 'aaaaaaa', 'Nam', '2024-10-29'),
(12, 2, 'dfsfdf', 'Nữ', '2024-11-18'),
(13, 1, 'fasdfds', 'Nam', '2024-11-06'),
(14, 2, 'fdfdfdf', 'Nữ', '2024-11-22'),
(15, 1, '', 'Nam', '2024-11-07'),
(16, 1, '', 'Nam', '2024-11-03'),
(17, 1, '', 'Nam', '2024-12-01'),
(18, 1, '', 'Nam', '2024-11-16'),
(19, 1, '', 'Nam', '2024-11-16'),
(20, 1, '', 'Nam', '2024-11-15'),
(21, 1, '', 'Nam', '2024-11-09'),
(22, 1, '', 'Nam', '2024-11-08'),
(23, 1, '', 'Nam', '2024-11-08'),
(24, 1, 'asdfsd', 'Nam', '2024-11-19'),
(25, 2, 'daf', 'Nữ', '2024-11-14'),
(26, 1, 'abc', 'Nam', '2024-11-27'),
(27, 1, 'abc', 'Nam', '2024-11-21'),
(28, 2, 'def', 'Nam', '2024-11-29'),
(29, 1, 'abc', 'Nam', '2024-11-28'),
(30, 1, 'def', 'Nữ', '2024-11-17'),
(31, 1, 'abc', 'Nam', '2024-10-31'),
(32, 2, 'df', 'Nam', '2024-12-04'),
(33, 3, 'ag', 'Nam', '2024-11-09'),
(34, 1, 'Ngọc Tú', 'Nam', '2002-02-06'),
(35, 3, 'Văn Trưởng', 'Nam', '2003-11-22'),
(36, 1, 'vu ngoc tu', 'Nam', '2003-07-30'),
(37, 1, 'vu ngoc tu', 'Nam', '2003-07-30'),
(38, 1, 'abc', 'Nam', '2003-07-30'),
(39, 1, 'vu ngoc tuan', 'Nam', '2003-07-30'),
(40, 1, 'Vũ Ngọc Tú', 'Nam', '2024-11-09'),
(41, 2, 'Hoàng Tuấn', 'Nam', '2024-11-23'),
(42, 3, 'Trịnh Trường', 'Nam', '2024-11-11'),
(43, 1, 'vu ngoc tuan', 'Nam', '2024-11-29'),
(44, 2, 'vu ngoc tu', 'Nam', '2024-11-07'),
(45, 1, 'vu ngoc tu', 'Nam', '2024-11-06'),
(46, 1, 'vu ngoc tu', 'Nam', '2024-11-06'),
(47, 1, 'vu ngoc tuan', 'Nam', '2024-11-20'),
(48, 1, 'abc', 'Nam', '2024-10-30'),
(49, 2, 'abc', 'Nam', '2024-11-27'),
(50, 3, 'Vũ Ngọc Tú', 'Nam', '2024-11-06'),
(51, 1, 'vu ngoc tu', 'Nam', '2024-11-19'),
(52, 1, 'vu ngoc tu', 'Nam', '2024-10-31'),
(53, 2, 'nguyen hoang tuan', 'Nam', '2024-11-28'),
(54, 1, 'a', 'Nam', '2024-11-25'),
(55, 2, 'b', 'Nam', '2024-11-28'),
(56, 1, 'ab', 'Nam', '2024-11-30'),
(57, 2, 'b', 'Nam', '2024-11-26'),
(58, 1, 'a', 'Nam', '2024-11-29');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `payments`
--

CREATE TABLE `payments` (
  `payment_id` int(11) NOT NULL,
  `payment_name` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `payments`
--

INSERT INTO `payments` (`payment_id`, `payment_name`) VALUES
(1, 'Thanh toán qua ngân hàng'),
(2, 'Thanh toán tiền mặt'),
(3, 'Thanh toán qua MoMo');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `paymentstatus`
--

CREATE TABLE `paymentstatus` (
  `payment_status_id` int(11) NOT NULL,
  `status_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `paymentstatus`
--

INSERT INTO `paymentstatus` (`payment_status_id`, `status_name`) VALUES
(1, 'Chờ thanh toán\r\n'),
(2, 'Đã thanh toán'),
(3, 'Đã hủy');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `permissions`
--

CREATE TABLE `permissions` (
  `permission_id` int(11) NOT NULL,
  `permission_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `permissions`
--

INSERT INTO `permissions` (`permission_id`, `permission_name`) VALUES
(1, 'Account Management'),
(2, 'Statistical Management'),
(3, 'Receipt Management\r\n'),
(4, 'Employee Management\r\n'),
(5, 'Feedback Management\r\n'),
(6, 'Customer Management\r\n'),
(7, 'Tour Management'),
(8, 'DashBoard Management\r\n');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `roleoperations`
--

CREATE TABLE `roleoperations` (
  `role_operation_id` int(11) NOT NULL,
  `role_id` int(11) DEFAULT NULL,
  `permission_id` int(11) DEFAULT NULL,
  `operation_id` int(11) DEFAULT NULL,
  `status_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `roleoperations`
--

INSERT INTO `roleoperations` (`role_operation_id`, `role_id`, `permission_id`, `operation_id`, `status_id`) VALUES
(21, 3, 4, 1, 1),
(22, 3, 4, 2, 1),
(23, 3, 4, 3, 1),
(31, 1, 1, 1, 1),
(32, 1, 3, 1, 1),
(33, 1, 3, 2, 1),
(35, 1, 4, 1, 1),
(36, 1, 5, 1, 1),
(37, 1, 7, 1, 1),
(38, 1, 8, 1, 1),
(39, 1, 6, 1, 1),
(64, 2, 8, 1, 1),
(68, 2, 4, 1, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `roles`
--

CREATE TABLE `roles` (
  `role_id` int(11) NOT NULL,
  `role_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `roles`
--

INSERT INTO `roles` (`role_id`, `role_name`) VALUES
(1, 'ROLE_ADMIN\r\n'),
(2, 'ROLE_STAFF\r\n'),
(3, 'ROLE_CUSTOMER');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `routes`
--

CREATE TABLE `routes` (
  `route_id` int(11) NOT NULL,
  `arrival_id` int(11) DEFAULT NULL,
  `departure_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `routes`
--

INSERT INTO `routes` (`route_id`, `arrival_id`, `departure_id`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 1),
(4, 3, 1),
(5, 3, 4),
(6, 6, 6),
(7, 7, 7),
(8, 8, 8),
(9, 9, 9),
(10, 10, 10);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `statusroleoperation`
--

CREATE TABLE `statusroleoperation` (
  `status_id` int(11) NOT NULL,
  `description` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `statusroleoperation`
--

INSERT INTO `statusroleoperation` (`status_id`, `description`) VALUES
(1, 'Open'),
(2, 'Block');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `tickets`
--

CREATE TABLE `tickets` (
  `booking_id` int(11) NOT NULL,
  `passenger_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `tickets`
--

INSERT INTO `tickets` (`booking_id`, `passenger_id`) VALUES
(17, 26),
(18, 27),
(18, 28),
(19, 29),
(19, 30),
(20, 31),
(20, 32),
(20, 33),
(21, 34),
(21, 35),
(22, 36),
(23, 37),
(24, 38),
(25, 39),
(26, 40),
(26, 41),
(26, 42),
(27, 43),
(27, 44),
(29, 46),
(29, 47),
(30, 48),
(30, 49),
(30, 50),
(31, 51),
(32, 52),
(32, 53),
(33, 54),
(33, 55),
(34, 56),
(34, 57),
(35, 58);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role_id` int(11) DEFAULT NULL,
  `email` varchar(255) NOT NULL,
  `status` int(11) NOT NULL,
  `verify_token` varchar(512) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `users`
--

INSERT INTO `users` (`user_id`, `username`, `password`, `role_id`, `email`, `status`, `verify_token`) VALUES
(112, 'admin', '$2a$10$wOLRQOEmPT3Nfx0w5F3Bi.sQpnLwq91i2kpwJ1deYRyAsUA0hXibG', 1, 'admin@gmail.com', 1, 'token'),
(114, 'Nguyễn Hoàng Tuấn', 'password account google', 3, 'nguyenhoangtuan1210003@gmail.com', 0, 'token'),
(116, 'hoangtuan', '$2a$10$.pIggvxBQCxZjQXzgd2Z2OYeWwArr8TIzORV/.nsSYb9FAQVb9ToG', 3, 'nguyenhoangtuan1210200@gmail.com', 0, 'eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJob2FuZ3R1YW4iLCJ1c2VyX2lkIjoxMTYsInNjb3BlIjoiUk9MRV9DVVNUT01FUiIsImlzcyI6ImhvYW5ndHVhbi5jb20iLCJleHAiOjE3MzI0NTkxODQsImlhdCI6MTczMjQ1MTk4NCwianRpIjoiZDI5MDFhYWUtYmFiZi00YmFiLWJjYzctYzlhZGE4Y2I3YTA1IiwiZW1haWwiOiJuZ3V5ZW5ob2FuZ3R1YW4xMjEwMjAwQGdtYWlsLmNvbSIsInVzZXJuYW1lIjoiaG9hbmd0dWFuIn0.4R1LwyTkl4vcx7g6O5BECgBKELSB9IzwEZ8wuKl9G8gvXkQGxsPmmaU5zdBRjX9y0NCjU8EiBdt0Ol0dTpULaQ'),
(117, 'test22', '$2a$10$aTuXq4usmwU6xC/O8OeQN.p4AVfoqGTKmCQsVHkRFFv42hVsiU02K', 3, 'tuannguyen@gmail.com', 1, 'token'),
(118, 'vungoctu', '$2a$10$N3E/gtq9NVQsg7aZx6FeIudPD/eyk3oI.Vxy7/EZnWMPmEHJjExVa', 3, 'vungoctu123@gmail.com', 1, 'eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ2dW5nb2N0dSIsInVzZXJfaWQiOjExOCwic2NvcGUiOiJST0xFX0NVU1RPTUVSIiwiaXNzIjoiaG9hbmd0dWFuLmNvbSIsImV4cCI6MTczMjY5NDM4MCwiaWF0IjoxNzMyNjg3MTgwLCJqdGkiOiI1ZmMzYTBhMS1lM2ExLTQzOWEtYmNkNy00NDJmYjgyZTcwYTUiLCJlbWFpbCI6InZ1bmdvY3R1MTJhM0BnbWFpbC5jb20iLCJ1c2VybmFtZSI6InZ1bmdvY3R1In0.kl_CLKZm4dzs2FasBDyJt_1_GW6p9oO_YvEhR594yJtf0m2VYE-gKCdGu-6aOmVUw-KDVUGzzjE3Eh_2xDD7qA'),
(119, 'NV_10', '$2a$10$4iZwbn6BUJbYF9TdUn/rreSrhwEEaypF/Bc/yKzRPLUi3fzfl7oQO', 3, 'tuannguyenit203@gmail.com', 1, 'toke'),
(120, 'NV_01', '$2a$10$UdDQ51lkoiZR7CXogPn4BuF0fbGOow/yzw5zMabAJzfgvTzLrH1Fm', 2, 'tuannguyenit23@gmail.com', 0, 'a'),
(121, 'NV_02', '$2a$10$81YbvCmA77Xvtg5oNd8JFeWOAPazwDDXiu400lOHu967TPiNopDLu', 2, 'trinhtruong2533@gmail.com', 0, 'a'),
(122, 'NV_03', '$2a$10$fuyN5QZQ5Bt5qJPsxy9kGObgDtALwwiAjTsUtvQSDOxkiCGTHxSX.', 2, 'trinhtruong2503@gmail.com', 0, 'a'),
(123, 'tuannguyen', '$2a$10$CFBpqQkkE3tnollTLvLT0uQI1hF9/NxQt5A1nTc0XVPO462Dgda42', 3, 'nguyenhoangtua2102003@gmail.com', 0, 'a'),
(124, 'lytruong', '$2a$10$WAQdOP9LVQG6t7qNWm8JpexexXa0hmBPl7DEosshFsq.jlE6LeXW.', 3, 'truonglykhong2003@gmail.com', 0, 'eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJseXRydW9uZyIsInVzZXJfaWQiOjEyNCwic2NvcGUiOiJST0xFX0NVU1RPTUVSIiwiaXNzIjoiaG9hbmd0dWFuLmNvbSIsImV4cCI6MTczMjkwNjA0MSwiaWF0IjoxNzMyODk4ODQxLCJqdGkiOiJjNmNlMTc3My03ZGU5LTRkMTMtODM0NC03YmMxOWQ1MTExMjQiLCJlbWFpbCI6InRydW9uZ2x5a2hvbmcyMDAzQGdtYWlsLmNvbSIsInVzZXJuYW1lIjoibHl0cnVvbmcifQ.OA0JEZsS1VLXu7m_l0mMFqZe2SqxzULA8vLBfgdiObJ2owpseOG7OjcwPOdV-sN_b7RU_AtVfbArP3iUFmqSDw'),
(125, 'trinhtruong', '$2a$10$gH.8b2ZFQ801PsuAp65Viuh6qZF8iVrDx3p/Uj7ItiSO75VIdw5MS', 3, 'trinhtruong25303', 0, 'eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJ0cmluaHRydW9uZyIsInVzZXJfaWQiOjEyNSwic2NvcGUiOiJST0xFX0NVU1RPTUVSIiwiaXNzIjoiaG9hbmd0dWFuLmNvbSIsImV4cCI6MTczMjkwNjEzNCwiaWF0IjoxNzMyODk4OTM0LCJqdGkiOiIyMTlkNzYxNi1hOWE2LTRkZWQtODVjMy1iYjVkZjZlZDZiNzUiLCJlbWFpbCI6InRyaW5odHJ1b25nMjUzMDMiLCJ1c2VybmFtZSI6InRyaW5odHJ1b25nIn0.uPWSe01awbS5USvrYbvj70SQ5OvKOiFyrmVxpUfuXtkmzSMqcw-iXGT68wCVfFaFGvf5NvD4vvr-LRtc2Hte0Q'),
(131, 'Tuấn Nguyễn', 'password account google', 3, 'tuannguyenit2003@gmail.com', 1, 'token'),
(133, '0552_Vũ Ngọc Tú', 'password account google', 3, 'vungoctu12a3@gmail.com', 1, 'a'),
(134, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_facebook', 3, 'ng@gmail.com', 1, 'a'),
(135, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_facebook', 3, 'b@gmail.com', 1, 'a'),
(136, 'hoangtuan123', '$2a$10$KNYoCxM3EWu8J7ql9I/x/eMxHqLG7IBUZEdrB1azLm4yTsA90L6.e', 3, 'nguyenabc@gmail.com', 0, 'a'),
(137, 'Nguyễn Hoàng Tuấn', 'password account google', 2, 'nguyen102003@gmail.com', 1, 'a'),
(138, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_facebook', 3, 'nguyenhangtuan12102003@gmail.com', 1, 'a'),
(139, 'Nguyễn Hoàng Tuấn', 'password account google', 3, 'nguyenhan12102003@gmail.com', 1, 'a'),
(141, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_facebook', 3, 'abcad@gmail.com', 1, 'a'),
(142, 'test11', '$2a$11$lsaz89a98au7u4HEt59Ig.HivjDgQWe0GL/Z/pba9xxVnEL5C/8dO', 3, 'tuan12102003@gmail.com@example.com', 1, 'a'),
(143, 'NV_06', '$2a$11$6ug6Bj5F/7455H9vSw3g2usSJ6vbM89FXBH5Ogf/LVIgMu.6/R5iW', 2, 'nguyenhoangtuan1202003@gmail.com@example.com', 1, 'a'),
(144, 'test12345', '$2a$11$8MMFwib7DcWs4isQxjDYnO0xiiZjmFBWRdNijqep9szqZTSetgXbG', 3, 'nguyenhoangtuan1202003@gmail.com@example.com', 1, 'a'),
(145, 'test123456', '$2a$11$DMTyX6.Z3ejXKsGEvgiOy.3g5WwCqsbWIovEpEyluqUJoNeI1QYYW', 3, '123@gmail.com', 1, 'a'),
(146, 'abcdef', '$2a$11$MbUZReJF6aWni50vXPGbMuLLywK7reyAhaqwc93NZXHKE0Z8cFxJW', 3, 'aa@gmail.com', 0, 'a'),
(147, 'abcdef', '$2a$11$kT4oYVQZJPzJ8F9yzgEVbus2L88bJzASWRXJlTB9cWCp9jt3yaJ8e', 3, 'bbb@gmail.com', 0, 'a'),
(148, 'abcdef', '$2a$11$PKRexgI6yXTKmo/VIOfHxe4qKzb8QZomRp54J/3vAzOjyvXvLNWwu', 3, 'ccc@gmail.com', 0, 'a'),
(149, 'abcdef', '$2a$11$5nyXBmqS4jgi1loNdWEBpeu6SF0/zhp6TcmIFJdL/TDYApqSczN6m', 3, 'cc@gmail.comc', 0, 'a'),
(150, 'abc', '$2a$11$g0DyaNfLj/wKSwqwtnX/J.PT9iHQ39CwF2vuKC2aUOhpFO39oZRWW', 3, 'bd@gmail.com', 0, 'a'),
(151, 'abc', '$2a$11$l/GDuuNLfpUS5clumUTR5OrxH..Ye4ms1cxtcjg0H52LVsNrMw.Uy', 3, 'dd@gmail.com', 0, 'a'),
(152, 'abc', '$2a$11$HWUJgig.Gna9Z4OlBmoYeOPI5rc2XcKoHClTpHrUVn7NLShY5tP.2', 3, 'ê@gmail.comê', 0, 'a'),
(153, 'abc', '$2a$11$hT7kxc0/uu0fwDyMITL5rO6BSkieOI6VUFeHWHr4cj4OGbMuktPqG', 3, 'gh@gmail.com', 0, 'a'),
(154, 'tuan123', '$2a$11$ARUNM3/2T0GCYJQzBKi9nuoEBzhFssBRNHCMlEsxXtiRihi9IYoJC', 3, '1235@gmail.com', 0, 'a'),
(155, 'tuan111', '$2a$11$NVj1PQ2v1zRah92GyzxBHOSlLBKnhzuLEZqaXH9M0NZVDWMuQR0B2', 3, '111@gmail.com', 0, 'a'),
(156, 'tuan555', '$2a$11$bt5fnXG3Kr70E.k9aChODO8R5WXYVA8y7mgmPZiGfYCI.QUnKDi9i', 3, '12366@gmail.com', 0, 'eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0dWFuNTU1IiwiaXNzIjoiaG9hbmd0dWFuLmNvbSIsImp0aSI6IjZhNzgwYzZjLThmODgtNDEwZC1iOTA3LTBjNzUzNjdkN2I4NiIsInVzZXJfaWQiOiIxNTYiLCJzY29wZSI6IlJPTEVfQ1VTVE9NRVIiLCJ1c2VybmFtZSI6InR1YW41NTUiLCJlbWFpbCI6Im5ndXllbmhvYW5ndHVhbjEyMTAyMDAzQGdtYWlsLmNvbSIsImV4cCI6MTczMzMwMDc1MiwiYXVkIjoiaG9hbmd0dWFuLmNvbSJ9.fmDGZuq4fmCY2zfimtKmFI-KTMZDywk0mbO9_Z-KtetsuQcTiGlGnKpK5ChtM0M0enUVr5KFh60-g4OzBKITtw'),
(158, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_google', 3, 'nguyenhoangtuan12102003@gmail.com', 1, 'a'),
(159, 'Tuan Nguyen', 'oauth2_default_password_google', 3, 'hoangtuan12803@gmail.com', 1, 'a'),
(160, 'Nguyễn Hoàng Tuấn', 'oauth2_default_password_facebook', 3, 'nguyenhoangtuan12102003@gmail.com', 1, 'a');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `arrivals`
--
ALTER TABLE `arrivals`
  ADD PRIMARY KEY (`arrival_id`),
  ADD KEY `status_id` (`status_id`);

--
-- Chỉ mục cho bảng `bookings`
--
ALTER TABLE `bookings`
  ADD PRIMARY KEY (`booking_id`),
  ADD KEY `customer_id` (`customer_id`),
  ADD KEY `payment_id` (`payment_id`),
  ADD KEY `fk_payment_status` (`payment_status_id`),
  ADD KEY `fk_bookings_detail_route` (`detail_route_id`);

--
-- Chỉ mục cho bảng `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`customer_id`),
  ADD KEY `FKrh1g1a20omjmn6kurd35o3eit` (`user_id`);

--
-- Chỉ mục cho bảng `departures`
--
ALTER TABLE `departures`
  ADD PRIMARY KEY (`departure_id`);

--
-- Chỉ mục cho bảng `detailroutes`
--
ALTER TABLE `detailroutes`
  ADD PRIMARY KEY (`detail_route_id`),
  ADD KEY `route_id` (`route_id`);

--
-- Chỉ mục cho bảng `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`employee_id`),
  ADD KEY `FK69x3vjuy1t5p18a5llb8h2fjx` (`user_id`);

--
-- Chỉ mục cho bảng `feedback`
--
ALTER TABLE `feedback`
  ADD PRIMARY KEY (`feedback_id`),
  ADD KEY `booking_id` (`booking_id`),
  ADD KEY `detail_route_id` (`detail_route_id`);

--
-- Chỉ mục cho bảng `images`
--
ALTER TABLE `images`
  ADD PRIMARY KEY (`image_id`),
  ADD KEY `fk_detailroute_images` (`detail_route_id`);

--
-- Chỉ mục cho bảng `invalidated_token`
--
ALTER TABLE `invalidated_token`
  ADD PRIMARY KEY (`id`);

--
-- Chỉ mục cho bảng `legs`
--
ALTER TABLE `legs`
  ADD PRIMARY KEY (`leg_id`),
  ADD KEY `detail_route_id` (`detail_route_id`);

--
-- Chỉ mục cho bảng `objects`
--
ALTER TABLE `objects`
  ADD PRIMARY KEY (`object_id`);

--
-- Chỉ mục cho bảng `operations`
--
ALTER TABLE `operations`
  ADD PRIMARY KEY (`operation_id`);

--
-- Chỉ mục cho bảng `passengers`
--
ALTER TABLE `passengers`
  ADD PRIMARY KEY (`passenger_id`),
  ADD KEY `object_id` (`object_id`);

--
-- Chỉ mục cho bảng `payments`
--
ALTER TABLE `payments`
  ADD PRIMARY KEY (`payment_id`);

--
-- Chỉ mục cho bảng `paymentstatus`
--
ALTER TABLE `paymentstatus`
  ADD PRIMARY KEY (`payment_status_id`);

--
-- Chỉ mục cho bảng `permissions`
--
ALTER TABLE `permissions`
  ADD PRIMARY KEY (`permission_id`);

--
-- Chỉ mục cho bảng `roleoperations`
--
ALTER TABLE `roleoperations`
  ADD PRIMARY KEY (`role_operation_id`),
  ADD KEY `role_id` (`role_id`),
  ADD KEY `permission_id` (`permission_id`),
  ADD KEY `operation_id` (`operation_id`);

--
-- Chỉ mục cho bảng `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`role_id`);

--
-- Chỉ mục cho bảng `routes`
--
ALTER TABLE `routes`
  ADD PRIMARY KEY (`route_id`),
  ADD KEY `arrival_id` (`arrival_id`),
  ADD KEY `departure_id` (`departure_id`);

--
-- Chỉ mục cho bảng `statusroleoperation`
--
ALTER TABLE `statusroleoperation`
  ADD PRIMARY KEY (`status_id`);

--
-- Chỉ mục cho bảng `tickets`
--
ALTER TABLE `tickets`
  ADD PRIMARY KEY (`booking_id`,`passenger_id`),
  ADD KEY `fk_tickets_passengers` (`passenger_id`);

--
-- Chỉ mục cho bảng `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD KEY `role_id` (`role_id`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `arrivals`
--
ALTER TABLE `arrivals`
  MODIFY `arrival_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `bookings`
--
ALTER TABLE `bookings`
  MODIFY `booking_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT cho bảng `customers`
--
ALTER TABLE `customers`
  MODIFY `customer_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=158;

--
-- AUTO_INCREMENT cho bảng `departures`
--
ALTER TABLE `departures`
  MODIFY `departure_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `detailroutes`
--
ALTER TABLE `detailroutes`
  MODIFY `detail_route_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT cho bảng `feedback`
--
ALTER TABLE `feedback`
  MODIFY `feedback_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT cho bảng `images`
--
ALTER TABLE `images`
  MODIFY `image_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=123;

--
-- AUTO_INCREMENT cho bảng `legs`
--
ALTER TABLE `legs`
  MODIFY `leg_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;

--
-- AUTO_INCREMENT cho bảng `objects`
--
ALTER TABLE `objects`
  MODIFY `object_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `operations`
--
ALTER TABLE `operations`
  MODIFY `operation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `passengers`
--
ALTER TABLE `passengers`
  MODIFY `passenger_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=59;

--
-- AUTO_INCREMENT cho bảng `payments`
--
ALTER TABLE `payments`
  MODIFY `payment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `paymentstatus`
--
ALTER TABLE `paymentstatus`
  MODIFY `payment_status_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `permissions`
--
ALTER TABLE `permissions`
  MODIFY `permission_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT cho bảng `roleoperations`
--
ALTER TABLE `roleoperations`
  MODIFY `role_operation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=69;

--
-- AUTO_INCREMENT cho bảng `roles`
--
ALTER TABLE `roles`
  MODIFY `role_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT cho bảng `routes`
--
ALTER TABLE `routes`
  MODIFY `route_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `statusroleoperation`
--
ALTER TABLE `statusroleoperation`
  MODIFY `status_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT cho bảng `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=161;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `arrivals`
--
ALTER TABLE `arrivals`
  ADD CONSTRAINT `arrivals_ibfk_1` FOREIGN KEY (`status_id`) REFERENCES `statusroleoperation` (`status_id`);

--
-- Các ràng buộc cho bảng `bookings`
--
ALTER TABLE `bookings`
  ADD CONSTRAINT `bookings_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  ADD CONSTRAINT `bookings_ibfk_2` FOREIGN KEY (`payment_id`) REFERENCES `payments` (`payment_id`),
  ADD CONSTRAINT `fk_bookings_detail_route` FOREIGN KEY (`detail_route_id`) REFERENCES `detailroutes` (`detail_route_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_payment_status` FOREIGN KEY (`payment_status_id`) REFERENCES `paymentstatus` (`payment_status_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Các ràng buộc cho bảng `customers`
--
ALTER TABLE `customers`
  ADD CONSTRAINT `FKrh1g1a20omjmn6kurd35o3eit` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Các ràng buộc cho bảng `detailroutes`
--
ALTER TABLE `detailroutes`
  ADD CONSTRAINT `FKqw4qu9wn037embauxjwx4agc1` FOREIGN KEY (`route_id`) REFERENCES `routes` (`route_id`);

--
-- Các ràng buộc cho bảng `employees`
--
ALTER TABLE `employees`
  ADD CONSTRAINT `FK69x3vjuy1t5p18a5llb8h2fjx` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Các ràng buộc cho bảng `feedback`
--
ALTER TABLE `feedback`
  ADD CONSTRAINT `feedback_ibfk_1` FOREIGN KEY (`booking_id`) REFERENCES `bookings` (`booking_id`),
  ADD CONSTRAINT `feedback_ibfk_2` FOREIGN KEY (`detail_route_id`) REFERENCES `detailroutes` (`detail_route_id`);

--
-- Các ràng buộc cho bảng `images`
--
ALTER TABLE `images`
  ADD CONSTRAINT `fk_detailroute_images` FOREIGN KEY (`detail_route_id`) REFERENCES `detailroutes` (`detail_route_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Các ràng buộc cho bảng `legs`
--
ALTER TABLE `legs`
  ADD CONSTRAINT `legs_ibfk_1` FOREIGN KEY (`detail_route_id`) REFERENCES `detailroutes` (`detail_route_id`);

--
-- Các ràng buộc cho bảng `passengers`
--
ALTER TABLE `passengers`
  ADD CONSTRAINT `passengers_ibfk_1` FOREIGN KEY (`object_id`) REFERENCES `objects` (`object_id`);

--
-- Các ràng buộc cho bảng `roleoperations`
--
ALTER TABLE `roleoperations`
  ADD CONSTRAINT `roleoperations_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`),
  ADD CONSTRAINT `roleoperations_ibfk_2` FOREIGN KEY (`permission_id`) REFERENCES `permissions` (`permission_id`),
  ADD CONSTRAINT `roleoperations_ibfk_3` FOREIGN KEY (`operation_id`) REFERENCES `operations` (`operation_id`);

--
-- Các ràng buộc cho bảng `routes`
--
ALTER TABLE `routes`
  ADD CONSTRAINT `routes_ibfk_1` FOREIGN KEY (`arrival_id`) REFERENCES `arrivals` (`arrival_id`),
  ADD CONSTRAINT `routes_ibfk_2` FOREIGN KEY (`departure_id`) REFERENCES `departures` (`departure_id`);

--
-- Các ràng buộc cho bảng `tickets`
--
ALTER TABLE `tickets`
  ADD CONSTRAINT `fk_tickets_passengers` FOREIGN KEY (`passenger_id`) REFERENCES `passengers` (`passenger_id`),
  ADD CONSTRAINT `tickets_ibfk_1` FOREIGN KEY (`booking_id`) REFERENCES `bookings` (`booking_id`);

--
-- Các ràng buộc cho bảng `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`role_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
