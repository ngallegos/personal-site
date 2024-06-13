#create an S3 bucket for the static site
resource "aws_s3_bucket" "static_site" {
    bucket = "${var.site_name}-personal-site-react-s3-${var.env}"
}

# make things public so we can host a static site
resource "aws_s3_bucket_public_access_block" "allow_public_access" {
    bucket = aws_s3_bucket.static_site.id
    block_public_acls       = true
    block_public_policy     = true
    ignore_public_acls      = true
    restrict_public_buckets = true
}

#configure the static site hosting from s3
resource "aws_s3_bucket_website_configuration" "static_site" {
    bucket = aws_s3_bucket.static_site.id
    index_document { 
        suffix = "index.html"
    }
    error_document { 
        key = "index.html" 
    }
}