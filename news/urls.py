from django.urls import path
from news import views

urlpatterns = [
    path('', views.post_list, name='post_list'),
]