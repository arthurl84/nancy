require 'rubygems'
require 'sinatra'

get '/' do
	"Hello from Pluralsight!"
end

get '/view' do
	erb :index

end