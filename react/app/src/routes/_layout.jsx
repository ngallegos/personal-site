import './site.css';
import { Outlet } from 'react-router-dom';

function Layout() {
  return (
    <div className="dark-theme font-sans quicksand">
      <div class="p-6 sm:p-10 md:p-16 flex flex-wrap">

        <div class="w-full md:w-1/3 md:pr-32 order-3 md:order-1">
            <div class="max-w-2xl md:float-right md:text-right leading-loose tracking-tight md:sticky md:top-0 ">
                <p class="font-bold my-4 md:my-12">Things To See</p>
                {/* <vc:nav-links link-type="@NavLinksType.Nav" ul-class="flex flex-wrap justify-between flex-col" li-class="nav"></vc:nav-links> */}
            </div>
        </div>
        <div class="w-full md:w-2/3 order-1 md:order-2">
            <div class="content max-w-2xl leading-loose tracking-tight">
                <Outlet/>
            </div>
        </div>
        {/* <vc:footer></vc:footer> */}
      </div>
    </div>
  );
}

export default Layout;
