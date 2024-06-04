import { getSiteMetadata } from '../util/contentUtil';
import './site.css';
import { Link, Outlet, useLoaderData } from 'react-router-dom';
import { SiteMetaData } from '../model/sitemetadata';


function Layout() {
  const meta = useLoaderData() as SiteMetaData;
  return (
    <div className="dark-theme font-sans quicksand">
      <div className="p-6 sm:p-10 md:p-16 flex flex-wrap">

        <div className="w-full md:w-1/3 md:pr-32 order-3 md:order-1">
            <div className="max-w-2xl md:float-right md:text-right leading-loose tracking-tight md:sticky md:top-0 ">
                <p className="font-bold my-4 md:my-12">Things To See</p>
                <ul className="flex flex-wrap justify-between flex-col">
                  {meta.navLinks.map((link, index) => (
                    <li key={index}>
                      {!!link.external ? <a className="nav" href={link.slug} target="_blank">{link.text}</a>
                      : <Link className="nav" to={link.slug}>{link.text}</Link>}
                    </li>
                  ))}
                </ul>
            </div>
        </div>
        <div className="w-full md:w-2/3 order-1 md:order-2">
            <div className="content max-w-2xl leading-loose tracking-tight">
                <Outlet/>
            </div>
        </div>
        {/* <vc:footer></vc:footer> */}
      </div>
    </div>
  );
}

export async function loader({ params }: any){
  const content = await getSiteMetadata();
  if (!content) throw new Response("", { status: 404 });
  return content;
}

export default Layout;
