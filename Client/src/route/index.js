import routes from "../config/route";
// client
import Home from "../pages/Client/Home";
import Login from "../pages/Client/Login";
import Register from "../pages/Client/Register";
import About from "../pages/Client/AboutUs";
import Payment from "../pages/Client/Payment";
import DetailTour from "../pages/Client/DetailTour";
import Tour from "../pages/Client/Tour";
// client profile
import Profile from "../pages/Client/Profile";
import Score from "../pages/Client/Profile/score";
import Discount from "../pages/Client/Profile/discount";
import Book from "../pages/Client/Profile/book";
import Refund from "../pages/Client/Profile/refund";
import BookingTour from "../pages/Client/OrderTour";
// layout
import DefaultLayout from "../layouts/DefaultLayout";
import AdminLayout from "../layouts/AdminLayout";
import ProfileLayout from "../layouts/ProfileLayout";
// admin
import DashBoard from "../pages/Admin/Dashboard";
import Receipt from "../pages/Admin/Receipt";
// 1 user
import ListUser from "../pages/Admin/User/ListUser";
import AddUser from "../pages/Admin/User/AddUser";
// 2 tour
import ListTour from "../pages/Admin/Tour/ListTour";
import AddTour from "../pages/Admin/Tour/AddTour";
import UpdateTour from "../pages/Admin/Tour/UpdateTour";
// 3 promotion
import Promotion from "../pages/Admin/Promotion";

import GoogleCallback from "../pages/Client/Login/GoogleCallback";

import FacebookCallback from "../pages/Client/Login/FacebookCallback";

import ListCustomer from "../pages/Admin/Customer";
import ListEmployee from "../pages/Admin/Employee";

import Decentralization from "../pages/Admin/Decentralization";
import ListFeedback from "../pages/Admin/Feedback";
import VerifyEmail from "../pages/Client/Register/verifyEmail";

import BookingDetail from "../pages/Admin/Receipt/detail";
import { Add } from "@mui/icons-material";
const publicRoutes = [
  // client routes
  { path: routes.home, component: Home, layout: DefaultLayout },
  { path: routes.route, component: Tour, layout: DefaultLayout },
  { path: routes.about_us, component: About, layout: DefaultLayout },
  { path: routes.detail_route, component: DetailTour, layout: DefaultLayout },
  { path: routes.login, component: Login, layout: null },
  { path: routes.register, component: Register, layout: null },
  { path: routes.payment, component: Payment, layout: null },
  { path: routes.booking_tour, component: BookingTour, layout: DefaultLayout },
  // profile
  { path: routes.profile, component: Profile, layout: ProfileLayout },
  { path: routes.discount, component: Discount, layout: ProfileLayout },
  { path: routes.score, component: Score, layout: ProfileLayout },
  { path: routes.book, component: Book, layout: ProfileLayout },
  { path: routes.refund, component: Refund, layout: ProfileLayout },

  // admin routes
  { path: routes.dashboard, component: DashBoard, layout: AdminLayout },
  { path: routes.receipt, component: Receipt, layout: AdminLayout },
  //  1 user
  { path: routes.add_user, component: AddUser, layout: AdminLayout },
  { path: routes.list_user, component: ListUser, layout: AdminLayout },
  //  2 tour
  { path: routes.add_tour, component: AddTour, layout: AdminLayout },
  { path: routes.list_tour, component: ListTour, layout: AdminLayout },
  { path: routes.update_tour, component: UpdateTour, layout: AdminLayout },

  // 3 promotion
  { path: routes.list_promotion, component: Promotion, layout: AdminLayout },

  { path: routes.google_callback, component: GoogleCallback, layout: null },
  //facebook
  { path: routes.facebook_callback, component: FacebookCallback, layout: null },
  //customer
  { path: routes.list_customer, component: ListCustomer, layout: AdminLayout },

  //employee
  { path: routes.list_employee, component: ListEmployee, layout: AdminLayout },

  { path: routes.list_feedback, component: ListFeedback, layout: AdminLayout },

  //admiin decentralization
  {
    path: routes.decentralization,
    component: Decentralization,
    layout: AdminLayout,
  }, //}

  // authentication user
  { path: routes.verify, component: VerifyEmail, layout: DefaultLayout },
  //list receipt
  { path: routes.list_receipt, component: BookingDetail, layout: AdminLayout },
];
export default publicRoutes;
